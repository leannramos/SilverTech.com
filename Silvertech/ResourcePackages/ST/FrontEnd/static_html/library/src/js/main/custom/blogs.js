export class Blogs {
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