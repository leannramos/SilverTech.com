
export const CaseStudyDownload = () => {
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
