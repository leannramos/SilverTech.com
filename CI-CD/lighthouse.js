"use strict";

const fs = require("fs");
const url = require("url");
const path = require("path");
const puppeteer = require("puppeteer");
const lighthouse = require("lighthouse");
const reportGenerator = require("lighthouse/lighthouse-core/report/report-generator");
const config = require('./config');

const PORT = 8041;

async function login(browser, url, tokenKey, accessToken) {
    const page = await browser.newPage();

    // setup dummy request/response
    await page.setRequestInterception(true);
    page.on("request", r => {
        r.respond({
            status: 200,
            contentType: "text/plain",
            body: "TEST"
        });
    });

    await page.goto(url);
    await page.waitFor(500);

    // save token to local storage
    await page.evaluate(
        (k, t) => {
            localStorage.setItem(k, t);
        },
        tokenKey,
        accessToken
    );

    await page.waitFor(500);
    await page.close();
}


/**
 * enable logging if needed
 * @param {*} enableLog 
 * @param {*} eventName 
 */
async function testPage(baseUrl, view, outputDir, enableLog, eventName) {
    try {
        const viewUrl = url.resolve(baseUrl, view.url);
        const outputFile = outputDir ? path.join(outputDir, view.name + '.html') : undefined;

        const flags = {
            port: PORT,
            emulatedFormFactor: "desktop",
            disableStorageReset: true,
            onlyCategories: ["performance", "best-practices"],
            throttlingMethod: "provided"
        };

        let retries = 0;
        let lhr = await lighthouse(viewUrl, flags);

        // convert fmp to number by using unary operator +
        let fmp = (+(lhr.lhr.audits['first-meaningful-paint'].numericValue / 1000).toFixed(2)); 
        let bps = lhr.lhr.categories['best-practices'].score * 100;

        while(isNaN(fmp) && retries < 3){
            process.stdout.write(`\nRetrying test for [${view.name}].. `);
            lhr = await lighthouse(viewUrl, flags);
            retries++;
        }
        
        if(retries > 0){
            fmp = (+(lhr.lhr.audits['first-meaningful-paint'].numericValue / 1000).toFixed(2)); 
            bps = lhr.lhr.categories['best-practices'].score * 100;
        }

        // test SLO breach
        const result = {
            success: true,
            fmp: fmp,
            bps: bps,
            messages: [
                `FMP: ${fmp} of max ${view.fmpSlo}s`,
                `BPS: ${bps} of min ${view.bpsSlo}`
            ]
        };

        if (fmp > view.fmpSlo || bps < view.bpsSlo) {
            result.success = false;
        }

        // if (enableLog) log results with logger of choice


        // write HTML report
        if (outputFile) {
            const html = reportGenerator.generateReport(lhr.lhr, "html");
            fs.writeFileSync(outputFile, html, {
                mode: 0o755
            });
        }
        
        return result;
    } catch (err) {
        console.log("Error: " + err.message);
        return {
            success: false,
            message: err.message
        }
    }
}

async function main() {
    const args = process.argv.slice(2);
    const url = args[0];
    const accessToken = args[1];
    const outputDir = args[2] || "./Output";
    const enableLog = args[3] === 'True'; 
    const eventName = args[4] || 'LighthouseResult';

    const browser = await puppeteer.launch({
        args: [
            `--remote-debugging-port=${PORT}`,
            "--incognito",
            "--no-sandbox"
        ],
        headless: true
    });

    let exitCode = 0;

    try {
        // authentication if needed
        // await login(browser, url, "", accessToken);

        // create output directory if does not exist
        if (!fs.existsSync(outputDir)) {
            console.log(`Creating directory [${outputDir}]`);
            fs.mkdirSync(outputDir);
        } else {
            console.log(`Using existing output directory [${outputDir}]`);
        }

        // run a test to ignore or normalize any side effects of running the first test
        try {
            await testPage(url, config.views[config.views.length - 1]);
        } catch {
            // ignore error
        }

        let failedMessages = [];

        // run a test on each view
        for (const view of config.views) {
            process.stdout.write(`\r\nRunning lighthouse test [${view.name}].. `);
            const result = await testPage(url, view, outputDir, enableLog, eventName);
            process.stdout.write(`${ result.success ? 'PASSED' : 'FAILED' }.. (fmp: ${ result.fmp }, bps: ${ result.bps })`);

            if (!result.success) {
                failedMessages.push(`[${view.name}] test FAILED with result [${result.messages.join(', ')}]`);
            }
        }

        if (failedMessages.length > 0) {
            failedMessages.map(m => console.error(m));
            exitCode = 1; // set non success exit code
        }

    } catch (err) {
        console.error("Error: " + err.message);
        exitCode = 1; // set non success exit code
    } finally {
        await new Promise((resolve) => setTimeout(resolve, 5000));
        await browser.close();
    }

    process.stdout.write('\n');
    process.exit(exitCode);
}

main();
