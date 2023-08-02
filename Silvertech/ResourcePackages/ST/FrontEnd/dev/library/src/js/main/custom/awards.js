export class Awards {
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