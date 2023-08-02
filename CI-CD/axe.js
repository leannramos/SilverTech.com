"use strict";

const fs = require("fs");
const url = require("url");
const path = require("path");
const config = require('./config');
const puppeteer = require("puppeteer");
const { AxePuppeteer } = require('axe-puppeteer');
const { convertAxeToSarif } = require('axe-sarif-converter');

const PORT = 8041;

async function login(page, url, tokenKey, accessToken) {
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
}

async function testPage(page, baseUrl, view, outputDir) {
    try {
        const outputFile = outputDir ? path.join(outputDir, view.name + '.sarif') : undefined;
        const viewUrl = url.resolve(baseUrl, view.url);
        
        // navigate to test page and run axe
        await page.goto(viewUrl);
        await page.waitFor(500);
        const axeResults = await new AxePuppeteer(page).analyze()
        let result = {
            success: axeResults.success,
            violations: []
        }

        // store only valid Fast Pass Accessibility Insights for Web violations
        axeResults.violations.forEach(v => {
            if (v.impact == 'serious' || v.impact == 'critical'){
                result.success = false;
                result.violations.push(v);
            }   
        });

        // set success - i.e. axe-scan failure for multiple violations, however none are critical or serious 
        if(result.violations.length == 0) {
            result.success = true;
        }

        // generate SARIF format report to show in Azure Pipelines 
        if (outputFile && !result.success) {
            axeResults.violations = result.violations;
            const sarifResults = convertAxeToSarif(axeResults);
            fs.writeFileSync(outputFile, JSON.stringify(sarifResults), {
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
        // const page = await browser.newPage();
        // await login(page, url, "", accessToken);

        // create output directory if does not exist
        if (!fs.existsSync(outputDir)) {
            console.log(`Creating directory [${outputDir}]`);
            fs.mkdirSync(outputDir);
        } else {
            console.log(`Using existing output directory [${outputDir}]`);
        }

        // run tests on all the views
        let failedMessages = [];
        for (const view of config.views) {
            const page = await browser.newPage();
            process.stdout.write(`\r\nRunning axe test [${view.name}].. `);
            const result = await testPage(page, url, view, outputDir);
            await page.waitFor(500);
            await page.close();

            // check if test succeeded and print messages 
            let i = 0; 
            process.stdout.write(`${ result.success ? `PASSED` : `FAILED: ${ result.violations.length } violation(s):\n`} `);
            if (!result.success) {
                result.violations.forEach(v => {
                    process.stdout.write(`\n\t${++i}) violationId: ${v.id}\n\t   desc: ${v.description}\n`);
                })

                // store failed messages 
                failedMessages.push(`[${view.name}] test FAILED with violations: ${result.violations.length}`);
            }
        }
        process.stdout.write(`\n`);

        // set non success exit code and log failed messages
        if (failedMessages.length > 0) {
            failedMessages.map(m => console.error(m));
            exitCode = 1;
        }
    } catch (err) {
        // set non success exit code
        console.error("Error: " + err.message);
        exitCode = 1;
    } finally {
        await browser.close();
    }

    process.exit(exitCode);
}

main();
