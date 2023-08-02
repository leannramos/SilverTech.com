/*
    :: Site Specific javascript modules
*/
import { ScrollToTop } from './scroll-to-top';
import { Menu } from './menu';
import { Blogs } from './blogs';
import { Awards } from './awards';
import { Newslisting } from './newslisting';
import { STWC } from './stwc';
import { GlobalChatIcon } from './global-chat';
import { AngleCards } from './angle-cards';
import { CaseStudySlider } from './case-study-slider';
import { Instafeed } from './instagram-feed';
import { HeroScrollDown } from './hero-scroll-down';
import { StTabs } from './st-tabs';
import { CaseStudyDownload } from './case-study-download';


export class Site {

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
