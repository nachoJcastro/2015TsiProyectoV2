
/*scroll to top*/

$(document).ready(function(){
	$(function () {
		$.scrollUp({
	        scrollName: 'scrollUp', // Element ID
	        scrollDistance: 300, // Distance from top/bottom before showing element (px)
	        scrollFrom: 'top', // 'top' or 'bottom'
	        scrollSpeed: 300, // Speed back to top (ms)
	        easingType: 'linear', // Scroll to top easing (see http://easings.net/)
	        animation: 'fade', // Fade, slide, none
	        animationSpeed: 200, // Animation in speed (ms)
	        scrollTrigger: false, // Set a custom triggering element. Can be an HTML string or jQuery object
					//scrollTarget: false, // Set a custom target element for scrolling to the top
	        scrollText: '<i class="fa fa-angle-up"></i>', // Text for element, can contain HTML
	        scrollTitle: false, // Set a custom <a> title if required.
	        scrollImg: false, // Set true to use image
	        activeOverlay: false, // Set CSS color to display scrollUp active point, e.g '#00FFFF'
	        zIndex: 700 // Z-Index for the overlay
		});
	});

    $('#datetimepicker1').datetimepicker({
        format: "YYYY-MM-DD hh:mm:ss",
        use24hours: true,
        });
	$('[data-toggle="tooltip"]').tooltip();


	//$(function () {
	//    var $container = $('#Container'),
    //        $controls = $('.controls');

	//    $container.mixItUp({
	//        animation: {
	//            duration: 700,
	//            effects: 'fade translateY(600%) stagger(35ms)',
	//            easing: 'cubic-bezier(0.86, 0, 0.07, 1)',
	//            reverseOut: true
	//        },
	//        controls: {
	//            enable: false
	//        }
	//    });

	//    $controls.on('click', '.filter', function () {
	//        var $btn = $(this),
    //            filter = $btn.attr('data-filter');

	//        $btn.addClass('active').siblings().removeClass('active');

	//        $container.mixItUp('filter', 'none', function () {
	//            $container.mixItUp('filter', filter);
	//        });
	//    });

	//});

	$('#carousel-606608').carousel({
	    interval: 5000
	});

    //Handles the carousel thumbnails
	$('[id^=carousel-selector-]').click(function () {
	    var id_selector = $(this).attr("id");
	    try {
	        var id = /-(\d+)$/.exec(id_selector)[1];
	        console.log(id_selector, id);
	        jQuery('#carousel-606608').carousel(parseInt(id));
	    } catch (e) {
	        console.log('Regex failed!', e);
	    }
	});
    // When the carousel slides, auto update the text
	$('#carousel-606608').on('slid.bs.carousel', function (e) {
	    var id = $('.item.active').data('slide-number');
	    $('#carousel-text').html($('#slide-content-' + id).html());
	});

	$('.flexslider').flexslider({
	    animation: 'fade', //String: Select your animation type, "fade" or "slide"
	    //Boolean: Create navigation for previous/next navigation? (true/false)
	    slideshowSpeed: 7000, //Integer: Set the speed of the slideshow cycling, in milliseconds
	    animationSpeed: 600, //Integer: Set the speed of animations, in milliseconds
	    pauseOnHover: false, //Boolean: Pause the slideshow when hovering over slider, then resume when no longer hovering
	    prevText: "", //String: Set the text for the "previous" directionNav item
	    nextText: "" //String: Set the text for the "next" directionNav item
	});

	$(".flex-control-nav").prepend('<a class="prev" href="/"><span class="fa fa-chevron-left"></span></a>');
	$(".flex-control-nav").append('<a class="next" href="/"><span class="fa fa-chevron-right"></span></a>');

	$(".flex-control-nav .prev").click(function () {
	    $(".flex-prev").trigger('click');
	});
	$(".flex-control-nav .next").click(function () {
	    $(".flex-next").trigger('click');
	});

	$(".flex-control-nav").wrap("<div class='container'></div>");

	$('.slider_re').slick();
	$('#zoom_0').zoom();
	$('#zoom_1').zoom();
	$('#zoom_2').zoom();

	var wow = new WOW(
     {
         boxClass: 'wow',      // default
         animateClass: 'animated', // default
         offset: 0,          // default
         mobile: true,       // default
         live: true        // default
     }
   )
	wow.init();

	$('#some-textarea').wysihtml5();

	$('#some-textarea').change(function () {
	    var desc = $('#some-textarea').val();
	    $('#descripcion').val(desc);
	});
});



