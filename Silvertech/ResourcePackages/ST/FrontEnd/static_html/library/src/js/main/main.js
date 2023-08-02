import { StCore } from './core/Core';
import { Site } from './custom/custom';
import { ImageLoader } from './core/ImageLoader';
import { shrinkPositions } from './custom/careers';

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
