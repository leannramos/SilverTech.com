export class Menu {
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