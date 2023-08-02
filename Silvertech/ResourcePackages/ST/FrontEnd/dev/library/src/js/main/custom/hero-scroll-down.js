export class HeroScrollDown {
    
    static main() {

        // Scroll To Content
        $('.icon.hero-scroll-trigger').on('click', function() {
            $('html, body').stop().animate({scrollTop: $("#hero-scroll-to").offset().top}, 500, 'linear');
        });
    }
}