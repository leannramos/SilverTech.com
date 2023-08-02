"use strict";

var _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();

function _asyncToGenerator(fn) { return function () { var gen = fn.apply(this, arguments); return new Promise(function (resolve, reject) { function step(key, arg) { try { var info = gen[key](arg); var value = info.value; } catch (error) { reject(error); return; } if (info.done) { resolve(value); } else { return Promise.resolve(value).then(function (value) { step("next", value); }, function (err) { step("throw", err); }); } } return step("next"); }); }; }

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

var Storage = function () {
    function Storage() {
        _classCallCheck(this, Storage);
    }

    _createClass(Storage, null, [{
        key: "storeItem",


        /*
            :: Local Storage
        */
        value: function storeItem(key, val) {
            if (this.localStorageIsSupported()) {
                localStorage.setItem(key, val);
            }
        }
    }, {
        key: "getItem",
        value: function getItem(key) {
            if (this.localStorageIsSupported()) {
                return localStorage.getItem(key);
            }
        }

        /*
            :: Cookies
        */

    }, {
        key: "setCookie",
        value: function setCookie(cname, cvalue, exdays) {
            var d = new Date();
            d.setTime(d.getTime() + exdays * 24 * 60 * 60 * 1000);
            var expires = "expires=" + d.toUTCString();
            document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
        }
    }, {
        key: "getCookie",
        value: function getCookie(cname) {
            var name = cname + "=";
            var ca = document.cookie.split(';');
            for (var i = 0; i < ca.length; i++) {
                var c = ca[i];
                while (c.charAt(0) === ' ') {
                    c = c.substring(1);
                }
                if (c.indexOf(name) === 0) {
                    return c.substring(name.length, c.length);
                }
            }
            return "";
        }

        /*
            :: Helpers
        */

    }, {
        key: "localStorageIsSupported",
        value: function localStorageIsSupported() {
            var testKey = 'test',
                storage = window.sessionStorage;
            try {
                storage.setItem(testKey, '1');
                storage.removeItem(testKey);
                return true;
            } catch (error) {
                return false;
            }
        }
    }]);

    return Storage;
}();

/*
    :: ADA Text Resizer
    --------------------
    The html gets a base font size of 62.5% to set a 1:10 font ratio
        EX: font-size: 20px; = font-size: 2rem;
    On increase or decrease, the html will get different classes

*/


var _storageKey = 'text_sizer_value';

var TextSizeChanger = function () {
    function TextSizeChanger() {
        _classCallCheck(this, TextSizeChanger);
    }

    _createClass(TextSizeChanger, null, [{
        key: "main",
        value: function main() {
            var _this = this;

            $('.text-size-button.increase').on('click', function (e) {
                e.preventDefault();
                _this.increaseFontSize();
            });
            $('.text-size-button.decrease').on('click', function (e) {
                e.preventDefault();
                _this.decreaseFontSize();
            });

            this.getSetting();
        }
    }, {
        key: "increaseFontSize",
        value: function increaseFontSize() {
            var html = $('html');

            if (html.hasClass("text-large")) {
                html.removeClass("text-large");
                html.addClass("text-larger");
                this.saveSetting("text-larger");
            } else {
                if (html.hasClass("text-larger")) {
                    html.removeClass("text-larger");
                    html.addClass("text-largest");
                    this.saveSetting("text-largest");
                } else {
                    if (!html.hasClass("text-largest")) {
                        html.addClass("text-large");
                        this.saveSetting("text-large");
                    }
                }
            }
        }
    }, {
        key: "decreaseFontSize",
        value: function decreaseFontSize() {
            var html = $('html');

            if (html.hasClass("text-large")) {
                html.removeClass("text-large");
                localStorage.removeItem(_storageKey);
            } else {
                if (html.hasClass("text-larger")) {
                    html.removeClass("text-larger");
                    html.addClass("text-large");
                    this.saveSetting("text-large");
                } else {
                    if (html.hasClass("text-largest")) {
                        html.removeClass("text-largest");
                        html.addClass("text-larger");
                        this.saveSetting("text-larger");
                    }
                }
            }
        }

        /*
            :: Helpers
        */

    }, {
        key: "saveSetting",
        value: function saveSetting(selector) {
            Storage.storeItem(_storageKey, selector);
        }
    }, {
        key: "getSetting",
        value: function getSetting(key) {
            var stored = Storage.getItem(_storageKey);
            if (stored) {
                $('html').addClass(stored);
            }
        }
    }]);

    return TextSizeChanger;
}();

/*
    :: Core ST Front End boilerplate Javascript Build
*/


var StCore = function () {
    function StCore() {
        _classCallCheck(this, StCore);
    }

    _createClass(StCore, null, [{
        key: "main",
        value: function main(options) {
            if (options.initialize.TextResizer) {
                TextSizeChanger.main();
            }
        }
    }]);

    return StCore;
}();

var ScrollToTop = function () {
    function ScrollToTop() {
        _classCallCheck(this, ScrollToTop);
    }

    _createClass(ScrollToTop, null, [{
        key: "main",
        value: function main() {

            // Scroll To Top
            $('.scrollToTop').on('click', function () {
                $('html, body').stop().animate({ scrollTop: 0 }, 500, 'linear');
                return false;
            });

            scrollToTopButton();

            // Fade in/out Scroll To Top button after 500px
            function scrollToTopButton() {
                var button = $('.scrollToTop'),
                    view = $(window),
                    timeoutKey = -1;

                // page load
                if (view.scrollTop() > 500) {
                    button.fadeIn();
                } else {
                    button.fadeOut();
                }

                $(document).on('scroll', function () {
                    if (timeoutKey) {
                        window.clearTimeout(timeoutKey);
                    }

                    timeoutKey = window.setTimeout(function () {
                        if (view.scrollTop() > 500) {
                            button.fadeIn();
                        } else {
                            button.fadeOut();
                        }
                    }, 100);
                });
            }
        }
    }]);

    return ScrollToTop;
}();

var Menu = function () {
    function Menu() {
        _classCallCheck(this, Menu);
    }

    _createClass(Menu, null, [{
        key: "main",
        value: function main() {

            OpenCloseMenu();

            // Open Menu 
            function OpenCloseMenu() {

                // Open Menu
                $(document).on('click', '.open-menu-btn', function () {
                    if ($('.hero-inner.case-study-detail').length) {
                        $('.main-menu').addClass('menu-shorter');
                    }
                    $('body').addClass('nav-open');
                    // $('.close-menu').focus();
                });

                // Close Menu
                $(document).on('click', '.close-menu', function () {
                    $('body').removeClass('nav-open');
                    // $('.open-menu-btn').focus();
                });
            }
        }
    }]);

    return Menu;
}();

var Blogs = function () {
    function Blogs() {
        _classCallCheck(this, Blogs);
    }

    _createClass(Blogs, null, [{
        key: "main",
        value: function main() {

            $(document).on({
                mouseenter: function mouseenter() {
                    $(this).closest('.caption').addClass('hover');
                },

                mouseleave: function mouseleave() {
                    $(this).closest('.caption').removeClass('hover');
                }
            }, '.blog-listing .blog .cta');
        }
    }]);

    return Blogs;
}();

var Awards = function () {
    function Awards() {
        _classCallCheck(this, Awards);
    }

    _createClass(Awards, null, [{
        key: "main",
        value: function main() {

            if (typeof $.fn.slick !== 'function') {
                return;
            }

            // Initialize Awards Slider
            $('.awards-slider').slick({
                autoplay: true,
                autoplaySpeed: 5000,
                speed: 300,
                infinite: true,
                dots: false,
                arrows: true,
                nextArrow: '<button type="button" role="button" aria-label="Next" class="slick-next default"><span class="arrow"></span></button>',
                prevArrow: '<button type="button" role="button" aria-label="Previous" class="slick-prev default"><span class="arrow"></span></button>',
                slidesToScroll: 6,
                slidesToShow: 6,
                pauseOnHover: true,
                responsive: [{
                    breakpoint: 992,
                    settings: {
                        slidesToShow: 6,
                        slidesToScroll: 6
                    }
                }, {
                    breakpoint: 769,
                    settings: {
                        slidesToShow: 3,
                        slidesToScroll: 3
                    }
                }, {
                    breakpoint: 481,
                    settings: {
                        slidesToShow: 2,
                        slidesToScroll: 2
                    }
                }],
                pauseOnDotsHover: true
            });

            // If you hover over slider image, fade all other items
            $('.awards-slider .item').hover(
            // mouse enter
            function () {
                $('.awards-slider').addClass('item-hovered');
                $(this).addClass('hovering');
                // mouse leave
            }, function () {
                $('.awards-slider').removeClass('item-hovered');
                $(this).removeClass('hovering');
            });
        }
    }]);

    return Awards;
}();

var Newslisting = function () {
    function Newslisting() {
        _classCallCheck(this, Newslisting);
    }

    _createClass(Newslisting, null, [{
        key: "main",
        value: function main() {

            // on hover of a news item, trigger overlay over photo and title to change color
            $(document).on({
                mouseenter: function mouseenter() {
                    $(this).closest('.news-item').addClass('hover');
                },

                mouseleave: function mouseleave() {
                    $(this).closest('.news-item').removeClass('hover');
                }
            }, '.news-listing .news-item a');
        }
    }]);

    return Newslisting;
}();

/*jshint esversion: 8 */


var STWC = function () {
    function STWC() {
        _classCallCheck(this, STWC);
    }

    _createClass(STWC, null, [{
        key: "main",
        value: function main() {
            var goTo = function () {
                var _ref = _asyncToGenerator( /*#__PURE__*/regeneratorRuntime.mark(function _callee(page) {
                    return regeneratorRuntime.wrap(function _callee$(_context) {
                        while (1) {
                            switch (_context.prev = _context.next) {
                                case 0:
                                    pause();
                                    imgNum = page;

                                    if (!$("#pauseBtn").hasClass("paused")) {
                                        $("ul.heroPages .button").addClass("paused");
                                    }

                                    start();
                                    restart();

                                case 5:
                                case "end":
                                    return _context.stop();
                            }
                        }
                    }, _callee, this);
                }));

                return function goTo(_x) {
                    return _ref.apply(this, arguments);
                };
            }();

            var start = function () {
                var _ref2 = _asyncToGenerator( /*#__PURE__*/regeneratorRuntime.mark(function _callee2() {
                    var loop, i, _i;

                    return regeneratorRuntime.wrap(function _callee2$(_context2) {
                        while (1) {
                            switch (_context2.prev = _context2.next) {
                                case 0:
                                    if (!inTransit) {
                                        _context2.next = 2;
                                        break;
                                    }

                                    return _context2.abrupt("return");

                                case 2:
                                    if (!($(window).width() > breakpoint)) {
                                        _context2.next = 30;
                                        break;
                                    }

                                    loop = $("#STWC .triangle").length;
                                    i = 1;

                                case 5:
                                    if (!(i <= loop)) {
                                        _context2.next = 12;
                                        break;
                                    }

                                    _context2.next = 8;
                                    return sleep(fillSpeed);

                                case 8:
                                    $("#STWC #tri_" + i).css("fill-opacity", ".95");

                                case 9:
                                    i++;
                                    _context2.next = 5;
                                    break;

                                case 12:
                                    _context2.next = 14;
                                    return sleep(switchSpeed);

                                case 14:
                                    $("#STWC #Path_4").css("fill", "url(#img" + imgNum + ")");

                                    $(".caption").removeClass("captionVisible");
                                    $("ul.heroPages li").removeClass("active").text("");
                                    $("ul.heroPages li").removeClass("non-clickable");

                                    _context2.next = 20;
                                    return sleep(switchSpeed);

                                case 20:
                                    _i = 1;

                                case 21:
                                    if (!(_i <= loop)) {
                                        _context2.next = 28;
                                        break;
                                    }

                                    _context2.next = 24;
                                    return sleep(fillSpeed);

                                case 24:
                                    $("#STWC #tri_" + _i).css("fill-opacity", ".25");

                                case 25:
                                    _i++;
                                    _context2.next = 21;
                                    break;

                                case 28:
                                    _context2.next = 32;
                                    break;

                                case 30:
                                    $(".caption").removeClass("captionVisible");
                                    $("ul.heroPages li").removeClass("active").text("");

                                case 32:

                                    $("#stwc_caption" + imgNum).addClass("captionVisible");
                                    $("#heroPage" + imgNum).addClass("active").text(imgNum);
                                    $("#heroPage" + imgNum).addClass("non-clickable");

                                    imgNum++;
                                    if (imgNum > imgCount) {
                                        imgNum = 1;
                                    }
                                    disabled = false;

                                case 38:
                                case "end":
                                    return _context2.stop();
                            }
                        }
                    }, _callee2, this);
                }));

                return function start() {
                    return _ref2.apply(this, arguments);
                };
            }();

            //Config Options
            var imgNum = 2;
            var imgCount = 0;
            var slideWait = 10000;
            var fillSpeed = 25;
            var switchSpeed = 25;
            var cInterval;
            var breakpoint = 512;
            var disabled = false;
            var inTransit = false;

            function sleep(ms) {
                return new Promise(function (resolve) {
                    return setTimeout(resolve, ms);
                });
            }

            function pause() {
                clearInterval(cInterval);
            }

            function restart() {
                cInterval = setInterval(start, slideWait);
            }

            $("ul.heroPages li:not(.button)").on('click', function () {
                if ($(this).hasClass('active') || $(this).hasClass('non-clickable') || disabled) {
                    return false;
                }
                disabled = true;
                $(this).siblings("li:not(.button)").addClass('non-clickable');
                var page = $(this).data("page");
                goTo(page);

                $(this).addClass('non-clickable');
                $(this).siblings("li:not(.button)").removeClass('non-clickable');
            });

            $("#pauseBtn").on('click', function () {
                if ($(this).hasClass('non-clickable')) {
                    return false;
                }

                var paused = $(this);
                paused.toggleClass("paused");

                if (!paused.hasClass("paused")) {
                    restart();
                    $(this).addClass('non-clickable');
                    var that = this;
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
    }]);

    return STWC;
}();

var GlobalChatIcon = function () {
    function GlobalChatIcon() {
        _classCallCheck(this, GlobalChatIcon);
    }

    _createClass(GlobalChatIcon, null, [{
        key: "main",
        value: function main() {
            var _COOKIE_NAME = 'ST_chat_dismissed';
            var cookie = getCookie(_COOKIE_NAME);
            var windowLocation = document.location.pathname;

            if (windowLocation === '/') {
                if (jQuery.isEmptyObject(cookie)) {
                    $('.global-chat').toggleClass('open');
                }
            }

            $(document).on('click', '.global-chat .chat-icon, .global-chat .close-btn', function (e) {
                e.preventDefault();
                $('.global-chat').toggleClass('open');
                setCookie(_COOKIE_NAME, 'true', 365);
            });

            /**
             * All params required; defaults to secure cookie;
             * 
             * @param {String} name 
             * @param {String} value 
             * @param {Number} valid 
             */
            function setCookie(name, value, valid) {
                var date = new Date();
                date.setTime(date.getTime() + valid * 24 * 60 * 60 * 1000);
                var expires = "expires=" + date.toUTCString();
                document.cookie = name + "=" + value + ";" + expires + "; secure; path=/";
            }

            /**
             * Name param required; returns cookie object
             * { key, value }
             * 
             * @param {String} name 
             * @return {Object}
             */
            function getCookie(name) {
                var decodedCookie = decodeURIComponent(document.cookie);
                var cookies = decodedCookie.split(';');
                var cookieObject = {};
                cookies.forEach(function (cookie) {
                    if (cookie.includes(name)) {
                        cookie = cookie.split('=');
                        cookie = {
                            key: cookie[0],
                            value: cookie[1]
                        };
                        cookieObject = cookie;
                    }
                });

                return cookieObject;
            }
        }
    }]);

    return GlobalChatIcon;
}();

var AngleCards = function () {
    function AngleCards() {
        _classCallCheck(this, AngleCards);
    }

    _createClass(AngleCards, null, [{
        key: "main",
        value: function main() {

            // if angle cards exist on page
            if ($('.angle-cards').length) {
                EqualHeightHeaders();

                $(window).resize(function () {
                    EqualHeightHeaders();
                });
            }

            // set equal heights to the angle cards headers
            function EqualHeightHeaders() {

                var maxHeight = 0;
                $('.angle-cards .angle-card header').css('height', 'auto');

                $('.angle-cards .angle-card header').each(function () {
                    var myHeight = $(this).outerHeight(true);
                    maxHeight = maxHeight > myHeight ? maxHeight : myHeight;
                });

                $('.angle-cards .angle-card header').css('height', maxHeight);
            }
        }
    }]);

    return AngleCards;
}();

var CaseStudySlider = function () {
    function CaseStudySlider() {
        _classCallCheck(this, CaseStudySlider);
    }

    _createClass(CaseStudySlider, null, [{
        key: "main",
        value: function main() {

            if ($('.portfolio-slider').length || typeof $.fn.slick === 'function') {

                var mySlider = $('.portfolio-slider');

                mySlider.slick({
                    infinite: true,
                    slidesToShow: 1,
                    slidesToScroll: 1,
                    autoplay: true,
                    autoplaySpeed: 5000,
                    dots: false,
                    arrows: true,
                    pauseOnHover: false,
                    prevArrow: '<button type="button" class="slick-prev"></button>',
                    nextArrow: '<button type="button" class="slick-next"></button>'
                });

                // Prev Btn
                $('.slider-controls .prevBtn').on('click', function (e) {
                    e.preventDefault();
                    mySlider.slick('slickPrev');
                    $(this).blur();
                });

                // Next Btn 
                $('.slider-controls .nextBtn').on('click', function (e) {
                    e.preventDefault();
                    mySlider.slick('slickNext');
                    $(this).blur();
                });

                // Pause Btn
                $('.slider-controls .pauseBtn').on('click', function (e) {
                    e.preventDefault();
                    if (!$(this).hasClass('active')) {
                        mySlider.slick('slickPause');
                        $(this).addClass('active');
                    } else {
                        mySlider.slick('slickPlay');
                        $(this).removeClass('active');
                    }
                    $(this).blur();
                });

                // Play Icon
                $('.slider-controls .playBtn').on('click', function (e) {
                    e.preventDefault();
                    mySlider.slick('slickPlay');
                    $('.slider-controls .pauseBtn').removeClass('active');
                    $(this).blur();
                });
            }
        }
    }]);

    return CaseStudySlider;
}();

var Instafeed = function () {
    function Instafeed() {
        _classCallCheck(this, Instafeed);
    }

    _createClass(Instafeed, null, [{
        key: "main",
        value: function main() {

            if (!$('.instagram-cards').length) {
                return;
            }

            $.instagramFeed({
                'username': 'silvertech_inc',
                'container': ".instagram-feed",
                'items': 3,
                'display_profile': false,
                'display_biography': false,
                'display_igtv': true,
                'styling': false
            });

            // remove focus when you click a instagram photo to remove animated styles
            $('.instagram-cards .instagram-feed .instagram_gallery a').on('click', function () {
                $(this).blur();
            });
        }
    }]);

    return Instafeed;
}();

var HeroScrollDown = function () {
    function HeroScrollDown() {
        _classCallCheck(this, HeroScrollDown);
    }

    _createClass(HeroScrollDown, null, [{
        key: "main",
        value: function main() {

            // Scroll To Content
            $('.icon.hero-scroll-trigger').on('click', function () {
                $('html, body').stop().animate({ scrollTop: $("#hero-scroll-to").offset().top }, 500, 'linear');
            });
        }
    }]);

    return HeroScrollDown;
}();

var StTabs = function StTabs() {
    var tabGroups = [];

    $('.st-tabs').each(function (index) {
        tabGroups.push(new StTabGroup($(this)));
        tabGroups[index].initialize();
    });
};

var StTabGroup = function () {
    function StTabGroup(element) {
        _classCallCheck(this, StTabGroup);

        this.element = element;
        this.tabs = element.find('li');
        this.indicator = element.find('.indicator');
        this.activeTabIndex = null;
        this.tabWidth = this.tabs.find('.st-tab-toggle').width();
        this.inTransit = false;
        this.mobileThreshold = 992;
        this.isMobile = $(window).width < this.mobileThreshold;
    }

    _createClass(StTabGroup, [{
        key: "initialize",
        value: function initialize() {
            var _this2 = this;

            this.setDefaultActive();

            $(window).on('resize', function () {
                _this2.reset();
            });
        }
    }, {
        key: "reset",
        value: function reset() {
            var _this3 = this;

            setTimeout(function () {
                _this3.tabWidth = _this3.tabs.find('.st-tab-toggle').width();

                _this3.tabs.each(function (index) {
                    var tab = _this3.tabs.eq(index);

                    if (tab.hasClass('active')) {
                        _this3.goToTab(tab, index, true);
                    }
                });
            }, 250);
        }

        /**
         * Set the appropriate default ui based on what item is marked active on load
         */

    }, {
        key: "setDefaultActive",
        value: function setDefaultActive() {
            var _this4 = this;

            this.tabs.each(function (index) {
                var tab = _this4.tabs.eq(index);
                var toggle = tab.find('.st-tab-toggle');

                if (tab.hasClass('active')) {
                    _this4.goToTab(tab, index);
                }

                // If non url link, prevent default action.
                toggle.on('click', function (e) {
                    var href = toggle.attr('href');
                    if (!href || href == '#' || href.trim() == '') {
                        e.preventDefault();
                        toggle.focus();
                    }
                });

                toggle.on('focus', function () {
                    _this4.goToTab(tab, index);
                });
            });
        }

        /**
         * Set active ui for a focused tab.
         * @param {jquery object} tab 
         * @param {number} index 
         */

    }, {
        key: "goToTab",
        value: function goToTab(tab, index) {
            var _this5 = this;

            var isReset = arguments.length > 2 && arguments[2] !== undefined ? arguments[2] : false;

            if (this.inTransit || index == this.activeTabIndex && !isReset) {
                return;
            }

            var heightVal = '5px';
            var transitHeighttVal = '2px';

            if ($(window).width() < this.mobileThreshold) {
                heightVal = '3px';
                transitHeighttVal = '1px';
            }

            this.inTransit = true;
            this.activeTabIndex = index;

            this.element.find('li.active').removeClass('active');

            if (!isReset) {
                this.indicator.css('height', transitHeighttVal);
                this.element.find('.st-tab-content').slideUp(250);
            }

            setTimeout(function () {
                _this5.indicator.css('transform', "translate(" + _this5.tabWidth * _this5.activeTabIndex + "px, -85%)");
            }, 100);

            setTimeout(function () {
                _this5.indicator.css('height', heightVal);
                tab.addClass('active');

                tab.find('.st-tab-content').slideDown(250);

                _this5.inTransit = false;
            }, 450);
        }
    }]);

    return StTabGroup;
}();

var CaseStudyDownload = function CaseStudyDownload() {
    //v-shaped-cta

    var downloadBtn = $('.download-case-study');

    if (!downloadBtn.length) {
        return;
    }

    // Get tirgger element.
    var triggerElement = null;

    if ($('.v-shaped-cta').length) {
        triggerElement = document.querySelector('.v-shaped-cta');
    } else if ($('.case-study-slider').length) {
        triggerElement = document.querySelector('.case-study-slider');
    } else if ($('.project-results').length) {
        triggerElement = document.querySelector('.project-results');
    } else {
        triggerElement = document.querySelector('.page-wrapper');
    }

    window.addEventListener('scroll', function () {
        if (triggerIsInView(triggerElement)) {
            setTimeout(function () {
                downloadBtn.addClass('show-btn');
            }, 200);
        }
    });

    if (triggerIsInView(triggerElement) || triggerIsInView(document.querySelector('.case-study-download-cta'))) {
        downloadBtn.addClass('show-btn');
    }
};

var triggerIsInView = function triggerIsInView(el) {
    var rect = el.getBoundingClientRect();
    var elemTop = rect.top;
    var elemBottom = rect.bottom;

    // Partially visible elements return true:
    var isVisible = elemTop < window.innerHeight && elemBottom >= 0;
    return isVisible;
};

/*
    :: Site Specific javascript modules
*/

var Site = function () {
    function Site() {
        _classCallCheck(this, Site);
    }

    _createClass(Site, null, [{
        key: "main",
        value: function main() {
            ScrollToTop.main();
            Menu.main();
            Blogs.main();
            Awards.main();
            Newslisting.main();
            STWC.main();
            GlobalChatIcon.main();
            AngleCards.main();
            CaseStudySlider.main();
            Instafeed.main();
            HeroScrollDown.main();
            StTabs();
            CaseStudyDownload();
        }
    }]);

    return Site;
}();

/**
 * Lazy loads images that are set up properly.
 * For img tags: 
 *      <img
            class="img-load-lazy"
            src="small-placeholder.jpeg"
			data-src="full-sized-actual-img.jpg"
			data-srcset="full-sized-actual-img.jpg 2x, mobile-sized-actual-img.jpg 1x"
            alt="">
        *Note: If no mobile image, just use the normal image src for both parts in the data-srcset attribute
            
    For BG Images: <div class="lazy-load-bg" style="background-image: url('/my-image.jpeg')"></div> 

    more info: https://developers.google.com/web/fundamentals/performance/lazy-loading-guidance/images-and-video
 */


var ImageLoader = function () {
    function ImageLoader() {
        var options = arguments.length > 0 && arguments[0] !== undefined ? arguments[0] : {};

        _classCallCheck(this, ImageLoader);

        this.queue = [];
        this.backgroundQueue = [];
        this.lazyImageObserver = null;
        this.lazyBgObserver = null;
        this.options = options;
    }

    _createClass(ImageLoader, [{
        key: "initialize",
        value: function initialize() {
            var _this6 = this;

            this.getImagesToLazyLoad();

            if (!("IntersectionObserver" in window)) {
                this.abort();
                return;
            }

            this.onContentLoaded();
            this.setGenericPlaceholder();

            this.queue.forEach(function (lazyImage) {
                _this6.lazyImageObserver.observe(lazyImage);
            });

            this.backgroundQueue.forEach(function (lazyBg) {
                _this6.lazyBgObserver.observe(lazyBg);
            });
        }
    }, {
        key: "getImagesToLazyLoad",
        value: function getImagesToLazyLoad() {
            this.queue = document.querySelectorAll('.img-load-lazy');
            this.backgroundQueue = document.querySelectorAll('.lazy-load-bg');
        }
    }, {
        key: "setGenericPlaceholder",
        value: function setGenericPlaceholder() {
            var _this7 = this;

            if (this.options.genericImagePlaceholder) {
                this.queue.forEach(function (img) {
                    img.setAttribute('src', _this7.options.genericImagePlaceholder);
                });
            }

            if (this.options.genericBgPlaceholder) {
                this.backgroundQueue.forEach(function (bg) {
                    bg.style.backgroundColor = _this7.options.genericBgPlaceholder;
                });
            }
        }
    }, {
        key: "onContentLoaded",
        value: function onContentLoaded() {
            var _this8 = this;

            this.lazyImageObserver = new IntersectionObserver(function (entries, observer) {
                entries.forEach(function (entry) {
                    if (entry.isIntersecting) {
                        var lazyImage = entry.target;
                        lazyImage.src = lazyImage.dataset.src;
                        lazyImage.srcset = lazyImage.dataset.srcset;
                        lazyImage.classList.remove('img-load-lazy');
                        _this8.lazyImageObserver.unobserve(lazyImage);
                    }
                });
            });

            this.lazyBgObserver = new IntersectionObserver(function (entries, observer) {
                entries.forEach(function (entry) {
                    if (entry.isIntersecting) {
                        entry.target.classList.remove('lazy-load-bg');
                        _this8.lazyBgObserver.unobserve(entry.target);
                    }
                });
            });
        }
    }, {
        key: "abort",
        value: function abort() {
            for (var i = 0; i < this.queue.length; i++) {
                this.queue[i].setAttribute('src', this.queue[i].getAttribute('data-src'));
                this.queue[i].classList.remove('img-load-lazy');
            }

            for (var _i2 = 0; _i2 < this.backgroundQueue.length; _i2++) {
                this.backgroundQueue[_i2].classList.remove('lazy-load-bg');
            }
        }
    }]);

    return ImageLoader;
}();

// Current openings arrows shrink to fit


var shrinkPositions = function shrinkPositions() {
    if (!ResizeObserver) return;

    document.querySelectorAll('.current-openings .positions').forEach(function (positions) {
        var resizeFunctions = Array.from(positions.querySelectorAll("a")).map(function (a) {
            var range = document.createRange();
            range.selectNodeContents(a);

            return function () {
                a.style.removeProperty('width');
                var width = range.getBoundingClientRect().width + 12;
                a.style.setProperty('width', width + "px");
            };
        });

        new ResizeObserver(function () {
            resizeFunctions.forEach(function (resize) {
                resize();
            });
        }).observe(positions);
    });
};

var MainScripts = function () {

    /*
        :: Initialize core st boilerplate modules
    */
    StCore.main({
        initialize: {
            TextResizer: true
        }
    });
    /*
        :: Initialize website/project specific modules
    */
    Site.main();

    var imageLoader = new ImageLoader({
        genericBgPlaceholder: '#f2f2f2',
        genericBgPlaceholderIsColor: true
    });

    setTimeout(function () {
        imageLoader.initialize();
    }, 500);

    shrinkPositions();
}();
//# sourceMappingURL=main.js.map
