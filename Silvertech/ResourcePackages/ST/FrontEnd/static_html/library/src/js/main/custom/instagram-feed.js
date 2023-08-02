export class Instafeed {
	static main() {

		if (!$('.instagram-cards').length) {
			return;
		}

	    $.instagramFeed({	    
	        'username': 'silvertech_inc',
	        'container': ".instagram-feed",
	        'items': 3,
	        'display_profile':false,
            'display_biography':false,
            'display_igtv':true,
            'styling':false
	    });

	    // remove focus when you click a instagram photo to remove animated styles
	    $('.instagram-cards .instagram-feed .instagram_gallery a').on('click', function() {
	    	$(this).blur();
	    });


	}
}