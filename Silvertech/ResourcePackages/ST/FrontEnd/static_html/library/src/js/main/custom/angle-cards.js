export class AngleCards {
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