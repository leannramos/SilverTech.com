/*jshint esversion: 8 */
export class STWC {
    static main() {

        //Config Options
        var imgNum = 2;
        var imgCount = 0;
        var slideWait = 10000;
        var fillSpeed = 25;
        var switchSpeed = 25;
        var cInterval;
        var prevPause = false;
        // bootstrap sm
        var breakpoint = 512;
        var disabled = false;
        var inTransit = false;

        function sleep(ms) {
            return new Promise(resolve => setTimeout(resolve, ms));
        }

        function pause() {
            clearInterval(cInterval);
        }

        function restart() {
            cInterval = setInterval(start, slideWait);
        }

        async function goTo(page) {
            pause();
            imgNum = page;

            if (!$("#pauseBtn").hasClass("paused")) {
                $("ul.heroPages .button").addClass("paused");
            }

            start();
            restart();
        }

        async function start() {
            if (inTransit) {
                return;
            }
            if ($(window).width() > breakpoint) {
                let loop = $("#STWC .triangle").length;
                for (let i = 1; i <= loop; i++) {
                    await sleep(fillSpeed);
                    $("#STWC #tri_" + i).css("fill-opacity", ".95");
                }

                await sleep(switchSpeed);
                $("#STWC #Path_4").css("fill", "url(#img" + imgNum + ")");

                $(".caption").removeClass("captionVisible");
                $("ul.heroPages li").removeClass("active").text("");
                $("ul.heroPages li").removeClass("non-clickable");

                await sleep(switchSpeed);

                for (let i = 1; i <= loop; i++) {
                    await sleep(fillSpeed);
                    $("#STWC #tri_" + i).css("fill-opacity", ".25");
                }
            } else {
                $(".caption").removeClass("captionVisible");
                $("ul.heroPages li").removeClass("active").text("");
            }

            $("#stwc_caption" + imgNum).addClass("captionVisible");
            $("#heroPage" + imgNum).addClass("active").text(imgNum);
            $("#heroPage" + imgNum).addClass("non-clickable");

            imgNum++;
            if (imgNum > imgCount) {
                imgNum = 1;
            }
            disabled = false;
        }

        $("ul.heroPages li:not(.button)").on('click', function () {
            if ($(this).hasClass('active') || $(this).hasClass('non-clickable') || disabled) {
                return false;
            }
            disabled = true;
            $(this).siblings("li:not(.button)").addClass('non-clickable');
            let page = $(this).data("page");
            goTo(page);

            $(this).addClass('non-clickable');
            $(this).siblings("li:not(.button)").removeClass('non-clickable');
        });

        $("#pauseBtn").on('click', function () {
            if ($(this).hasClass('non-clickable')) {
                return false;
            }

            let paused = $(this);
            paused.toggleClass("paused");

            if (!paused.hasClass("paused")) {
                restart();
                $(this).addClass('non-clickable');
                let that = this;
                setTimeout(function () {
                    $(that).removeClass('non-clickable');
                }, 3000);
            } else {
                pause();
            }
        });

        document.addEventListener("visibilitychange", function () {
            inTransit = true;

            if (document.visibilityState == 'visible') {
                inTransit = false;
                $("#pauseBtn").click();
            } else {
                if (!$("#pauseBtn").hasClass('paused')) {
                    $("#pauseBtn").click();
                }
                
            }
        }, false);

        imgCount = $("#STWC defs pattern").length;

        if (imgCount > 0) {
            $("#STWC #Path_4").css("fill", "url(#img1)");
        }

        $("#pauseBtn").click();
    }
}