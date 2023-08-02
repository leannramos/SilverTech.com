export class CaseStudySlider {
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