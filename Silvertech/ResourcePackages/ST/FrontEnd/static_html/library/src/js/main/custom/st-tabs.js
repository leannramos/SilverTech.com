
export const StTabs = () => {
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
