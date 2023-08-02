export class Newslisting {
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