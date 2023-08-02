class Storage {

    /*
        :: Local Storage
    */
    static storeItem(key, val) {
        if (this.localStorageIsSupported()) {
            localStorage.setItem(key, val);
        }
    }

    static getItem(key) {
        if (this.localStorageIsSupported()) {
            return localStorage.getItem(key);
        }
    }

    /*
        :: Cookies
    */
    static setCookie(cname, cvalue, exdays) {
        const d = new Date();
        d.setTime(d.getTime() + (exdays*24*60*60*1000));
        const expires = "expires="+d.toUTCString();
        document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
    }

    static getCookie(cname) {
        var name = cname + "=";
        var ca = document.cookie.split(';');
        for(var i = 0; i < ca.length; i++) {
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
    static localStorageIsSupported() {
        let testKey = 'test', storage = window.sessionStorage;
        try {
            storage.setItem(testKey, '1');
            storage.removeItem(testKey);
            return true;
        } catch (error) {
            return false;
        }
    }

}

/*
    :: ADA Text Resizer
    --------------------
    The html gets a base font size of 62.5% to set a 1:10 font ratio
        EX: font-size: 20px; = font-size: 2rem;
    On increase or decrease, the html will get different classes

*/
const _storageKey = 'text_sizer_value';

class TextSizeChanger {

    static main() {
        $('.text-size-button.increase').on('click', (e) => {
            e.preventDefault();
            this.increaseFontSize();
        });
        $('.text-size-button.decrease').on('click', (e) => {
            e.preventDefault();
            this.decreaseFontSize();
        });

        this.getSetting();
    }

    static increaseFontSize() {
        const html = $(('html'));

        if(html.hasClass("text-large")) {
            html.removeClass("text-large");
            html.addClass("text-larger");
            this.saveSetting("text-larger");
        }
        else {
            if(html.hasClass("text-larger")) {
                html.removeClass("text-larger");
                html.addClass("text-largest");
                this.saveSetting("text-largest");
            }
            else {
                if(!html.hasClass("text-largest")) {
                    html.addClass("text-large");
                    this.saveSetting("text-large");
                }
            }
        }
    }

    static decreaseFontSize() {
        const html = $(('html'));

        if(html.hasClass("text-large")) {
            html.removeClass("text-large");
            localStorage.removeItem(_storageKey);
        }
        else {
            if(html.hasClass("text-larger")) {
                html.removeClass("text-larger");
                html.addClass("text-large");
                this.saveSetting("text-large");
            }
            else {
                if(html.hasClass("text-largest")) {
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
    static saveSetting(selector) {
        Storage.storeItem(_storageKey, selector);
    }

    static getSetting(key) {
        let stored = Storage.getItem(_storageKey);
        if (stored) {
            $(('html')).addClass(stored);
        }
    }

}

/*
    :: Core ST Front End boilerplate Javascript Build
*/
class StCore {

    static main(options) {
        if (options.initialize.TextResizer) {
            TextSizeChanger.main();
        }
    }

}

class ScrollToTop {

    static main() {
        
    	// Scroll To Top
    	$('.scrollToTop').on('click', function(){
    	    $('html, body').stop().animate({scrollTop: 0}, 500, 'linear');
    	    return false;
    	});

       scrollToTopButton();

    	// Fade in/out Scroll To Top button after 500px
    	function scrollToTopButton() {
    	    var button  = $('.scrollToTop'), 
    	        view = $(window),
    	        timeoutKey = -1;

    	    // page load
    	    if (view.scrollTop() > 500) {
    	        button.fadeIn();
    	    }
    	    else {
    	        button.fadeOut();
    	    }

    	    $(document).on('scroll', function() {
    	        if(timeoutKey) {
    	            window.clearTimeout(timeoutKey);
    	        }

    	        timeoutKey = window.setTimeout(function(){
    	            if (view.scrollTop() > 500) {
    	                button.fadeIn();
    	            }
    	            else {
    	                button.fadeOut();
    	            }
    	        }, 100);
    	    });
    	}
	    
    }

}

class Menu {
	static main() {

		OpenCloseMenu();

		// Open Menu 
		function OpenCloseMenu() {

			// Open Menu
			$(document).on('click', '.open-menu-btn', function() {
				if ($('.hero-inner.case-study-detail').length) {
					$('.main-menu').addClass('menu-shorter');
				}
				$('body').addClass('nav-open');
				// $('.close-menu').focus();
			});

			// Close Menu
			$(document).on('click', '.close-menu', function() {
				$('body').removeClass('nav-open');
				// $('.open-menu-btn').focus();
			});
		}

	}
}

class Blogs {
	static main() {

		$(document).on({
		    mouseenter: function () {
		        $(this).closest('.caption').addClass('hover');
		    },

		    mouseleave: function () {
		        $(this).closest('.caption').removeClass('hover');
		    }
		}, '.blog-listing .blog .cta');

	}
}

class Awards {
	static main() {

		if ((typeof $.fn.slick !== 'function')) {
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
			slidesToScroll:6,
			slidesToShow:6,
			pauseOnHover: true,
			responsive: [
				{
				  breakpoint: 992,
				  settings: {
					slidesToShow: 6,
					slidesToScroll: 6,  
				  }
				},
				{
				  breakpoint: 769,
				  settings: {
					slidesToShow: 3,
					slidesToScroll: 3
				  }
				},
				{
				  breakpoint: 481,
				  settings: {
					slidesToShow: 2,
					slidesToScroll: 2
				  }
				}
			],
			pauseOnDotsHover: true
		});

		// If you hover over slider image, fade all other items
		$('.awards-slider .item').hover(
		  	// mouse enter
		  	function() {
		  		$('.awards-slider').addClass('item-hovered');
		    	$(this).addClass('hovering');
		  	// mouse leave
		  	}, function() {
		  		$('.awards-slider').removeClass('item-hovered');
		    	$(this).removeClass('hovering');
		  	}
		);

	}
}

class Newslisting {
	static main() {

		// on hover of a news item, trigger overlay over photo and title to change color
		$(document).on({
		    mouseenter: function () {
		        $(this).closest('.news-item').addClass('hover');
		    },

		    mouseleave: function () {
		        $(this).closest('.news-item').removeClass('hover');
		    }
		}, '.news-listing .news-item a');

	}
}

/*jshint esversion: 8 */
class STWC {
    static main() {

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

class GlobalChatIcon {
	static main() {
		const _COOKIE_NAME = 'ST_chat_dismissed';
		const cookie = getCookie(_COOKIE_NAME);
		const windowLocation = document.location.pathname;

		if (windowLocation === '/') {
			if (jQuery.isEmptyObject(cookie)) {
				$('.global-chat').toggleClass('open');
			}
		}

		$(document).on('click', '.global-chat .chat-icon, .global-chat .close-btn', function(e) {
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
			let date = new Date();
			date.setTime(date.getTime() + (valid*24*60*60*1000));
			const expires = "expires="+ date.toUTCString();
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
			const decodedCookie = decodeURIComponent(document.cookie);
			const cookies = decodedCookie.split(';');
			let cookieObject = {};
			cookies.forEach(cookie => {
				if(cookie.includes(name)) {
					cookie = cookie.split('=');
					cookie = {
						key: cookie[0],
						value: cookie[1],
					};
					cookieObject = cookie;
				}
			});

			return cookieObject;
		}

	}
}

class AngleCards {
	static main() {

		// if angle cards exist on page
		if ( $('.angle-cards').length) {
			EqualHeightHeaders();

			$( window ).resize(function() {
				EqualHeightHeaders();
			});
		}


		// set equal heights to the angle cards headers
		function EqualHeightHeaders() {
			
			let maxHeight = 0;
			$('.angle-cards .angle-card header').css('height', 'auto');

			$('.angle-cards .angle-card header').each(function() {
				let myHeight = $(this).outerHeight(true);
				maxHeight = (maxHeight > myHeight) ? maxHeight:myHeight;
			});

			$('.angle-cards .angle-card header').css('height', maxHeight);
		}

	}
}

class CaseStudySlider {
	static main() {

		if ( $('.portfolio-slider').length || (typeof $.fn.slick === 'function')) {

			const mySlider = $('.portfolio-slider');

			mySlider.slick({
				infinite: true,
				slidesToShow: 1,
				slidesToScroll: 1,
				autoplay : true,
				autoplaySpeed : 5000,
				dots: false,
				arrows: true,
				pauseOnHover : false,
				prevArrow : '<button type="button" class="slick-prev"></button>',
				nextArrow : '<button type="button" class="slick-next"></button>'
			});


			// Prev Btn
			$('.slider-controls .prevBtn').on('click', function(e) {
				e.preventDefault();
				mySlider.slick('slickPrev');
				$(this).blur();
			});

			// Next Btn 
			$('.slider-controls .nextBtn').on('click', function(e) {
				e.preventDefault();
				mySlider.slick('slickNext');
				$(this).blur();
			});

			// Pause Btn
			$('.slider-controls .pauseBtn').on('click', function(e) {
				e.preventDefault();
				if (!$(this).hasClass('active')) {
					mySlider.slick('slickPause');
					$(this).addClass('active');
				}
				else {
					mySlider.slick('slickPlay');					
					$(this).removeClass('active');
				}
				$(this).blur();
			});

			// Play Icon
			$('.slider-controls .playBtn').on('click', function(e) {
				e.preventDefault();
				mySlider.slick('slickPlay');
				$('.slider-controls .pauseBtn').removeClass('active');
				$(this).blur();
			});

		}

	}
}

class Instafeed {
	static main() {

		if (!$('.instagram-cards').length) {
			return;
		}

	    $.instagramFeed({	    
	        'username': 'silvertech_inc',
	        'container': ".instagram-feed",
	        'items': 3,
	        'display_profile':false,
            'display_biography':false,
            'display_igtv':true,
            'styling':false
	    });

	    // remove focus when you click a instagram photo to remove animated styles
	    $('.instagram-cards .instagram-feed .instagram_gallery a').on('click', function() {
	    	$(this).blur();
	    });


	}
}

class HeroScrollDown {
    
    static main() {

        // Scroll To Content
        $('.icon.hero-scroll-trigger').on('click', function() {
            $('html, body').stop().animate({scrollTop: $("#hero-scroll-to").offset().top}, 500, 'linear');
        });
    }
}

const StTabs = () => {
    const tabGroups = [];

    $('.st-tabs').each(function(index) {
        tabGroups.push(new StTabGroup($(this)));
        tabGroups[index].initialize();
    });
};

class StTabGroup {

    constructor(element) {
        this.element = element;
        this.tabs = element.find('li');
        this.indicator = element.find('.indicator');
        this.activeTabIndex = null;
        this.tabWidth = this.tabs.find('.st-tab-toggle').width();
        this.inTransit = false;
        this.mobileThreshold = 992;
        this.isMobile =  $(window).width < this.mobileThreshold;
    }

    initialize() {
        this.setDefaultActive();

        $(window).on('resize', () => {
            this.reset();
        });
    }

    reset() {
        setTimeout(() => {
            this.tabWidth = this.tabs.find('.st-tab-toggle').width();

            this.tabs.each((index) => {
                const tab = this.tabs.eq(index);

                if (tab.hasClass('active')) {
                    this.goToTab(tab, index, true);
                }
            });
        }, 250);
    }

    /**
     * Set the appropriate default ui based on what item is marked active on load
     */
    setDefaultActive() {
        this.tabs.each((index) => {
            const tab = this.tabs.eq(index);
            const toggle = tab.find('.st-tab-toggle');

            if (tab.hasClass('active')) {
                this.goToTab(tab, index);
            }

            // If non url link, prevent default action.
            toggle.on('click', (e) => {
                const href = toggle.attr('href');
                if (!href || href == '#' || href.trim() == '') {
                    e.preventDefault();
                    toggle.focus();
                }
            });

            toggle.on('focus', () => {
                this.goToTab(tab, index);
            });
        });
    }

    /**
     * Set active ui for a focused tab.
     * @param {jquery object} tab 
     * @param {number} index 
     */
    goToTab(tab, index, isReset = false) {
        if (this.inTransit || ((index == this.activeTabIndex) && !isReset)) {
            return;
        }

        let heightVal = '5px';
        let transitHeighttVal = '2px';

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

        setTimeout(() => {
            this.indicator.css('transform', `translate(${(this.tabWidth * this.activeTabIndex)}px, -85%)`);
        }, 100);

        setTimeout(() => {
            this.indicator.css('height', heightVal);
            tab.addClass('active');

            tab.find('.st-tab-content').slideDown(250);

            this.inTransit = false;
        }, 450);
    }

}

const CaseStudyDownload = () => {
//v-shaped-cta

    const downloadBtn = $('.download-case-study');

    if (!downloadBtn.length) {
        return;
    }

    // Get tirgger element.
    let triggerElement = null;

    if ($('.v-shaped-cta').length) {
        triggerElement = document.querySelector('.v-shaped-cta');
    } else if ($('.case-study-slider').length) {
        triggerElement = document.querySelector('.case-study-slider');
    } else if ($('.project-results').length) {
        triggerElement = document.querySelector('.project-results');
    } else {
        triggerElement = document.querySelector('.page-wrapper');
    }

    window.addEventListener('scroll', () => {
        if (triggerIsInView(triggerElement)) {
            setTimeout(() => {
                downloadBtn.addClass('show-btn');
            }, 200);
        }
    });

    if (triggerIsInView(triggerElement) || triggerIsInView(document.querySelector('.case-study-download-cta'))) {
        downloadBtn.addClass('show-btn');
    }

};

const triggerIsInView = (el) => {
    const rect = el.getBoundingClientRect();
    const elemTop = rect.top;
    const elemBottom = rect.bottom;
    
    // Partially visible elements return true:
    const isVisible = elemTop < window.innerHeight && elemBottom >= 0;
    return isVisible;
  };

/*
    :: Site Specific javascript modules
*/
class Site {

    static main() {
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

}

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
class ImageLoader {

    constructor(options = {}) {
        this.queue = [];
        this.backgroundQueue = [];
        this.lazyImageObserver = null;
        this.lazyBgObserver = null;
        this.options = options;
    }

    initialize() {
        this.getImagesToLazyLoad();

        if (!("IntersectionObserver" in window)) {
            this.abort();
            return;
        }

        this.onContentLoaded();
        this.setGenericPlaceholder();

        this.queue.forEach((lazyImage) => {
            this.lazyImageObserver.observe(lazyImage);
        });

        this.backgroundQueue.forEach((lazyBg) => {
            this.lazyBgObserver.observe(lazyBg);
        });
    }

    getImagesToLazyLoad() {
        this.queue = document.querySelectorAll('.img-load-lazy');
        this.backgroundQueue = document.querySelectorAll('.lazy-load-bg');
    }

    setGenericPlaceholder() {
        if (this.options.genericImagePlaceholder) {
            this.queue.forEach((img) => {
                img.setAttribute('src', this.options.genericImagePlaceholder);
            });
        }

        if (this.options.genericBgPlaceholder) {
            this.backgroundQueue.forEach((bg) => {
                bg.style.backgroundColor = this.options.genericBgPlaceholder;
            });
        }
    }

    onContentLoaded() {
        this.lazyImageObserver = new IntersectionObserver((entries, observer) => {
            entries.forEach((entry) => {
                if (entry.isIntersecting) {
                    let lazyImage = entry.target;
                    lazyImage.src = lazyImage.dataset.src;
                    lazyImage.srcset = lazyImage.dataset.srcset;
                    lazyImage.classList.remove('img-load-lazy');
                    this.lazyImageObserver.unobserve(lazyImage);
                }
            });
        });

        this.lazyBgObserver = new IntersectionObserver((entries, observer) => {
            entries.forEach((entry) => {
                if (entry.isIntersecting) {
                    entry.target.classList.remove('lazy-load-bg');
                    this.lazyBgObserver.unobserve(entry.target);
                }
            });
        });
    }

    abort() {
        for (let i = 0; i < this.queue.length; i++) {
            this.queue[i].setAttribute('src', this.queue[i].getAttribute('data-src'));
            this.queue[i].classList.remove('img-load-lazy');
        }

        for (let i = 0; i < this.backgroundQueue.length; i++) {
            this.backgroundQueue[i].classList.remove('lazy-load-bg');
        }
    }

}

// Current openings arrows shrink to fit
const shrinkPositions = () => {
    if (!ResizeObserver)
        return;

    document.querySelectorAll('.current-openings .positions')
        .forEach(positions => {
            const resizeFunctions =
                Array.from(positions.querySelectorAll("a"))
                    .map(a => {
                        const range = document.createRange();
                        range.selectNodeContents(a);

                        return () => {
                            a.style.removeProperty('width');
                            const width = range.getBoundingClientRect().width + 12;
                            a.style.setProperty('width', `${width}px`);
                        };
                    });

            new ResizeObserver(() => {
                resizeFunctions.forEach(resize => {
                    resize();
                });
            }).observe(positions);
        });
};

const MainScripts = (() => {

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

    const imageLoader = new ImageLoader({
        genericBgPlaceholder: '#f2f2f2',
        genericBgPlaceholderIsColor: true
    });
    
    setTimeout(() => {
        imageLoader.initialize();
    }, 500);

    shrinkPositions();

})();
