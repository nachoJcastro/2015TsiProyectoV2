/*price range*/

 $('#sl2').slider();

	var RGBChange = function() {
	  $('#RGB').css('background', 'rgb('+r.getValue()+','+g.getValue()+','+b.getValue()+')')
	};	
		
/*scroll to top*/

$(document).ready(function(){
	//$(function () {
	//	$.scrollUp({
	//        scrollName: 'scrollUp', // Element ID
	//        scrollDistance: 300, // Distance from top/bottom before showing element (px)
	//        scrollFrom: 'top', // 'top' or 'bottom'
	//        scrollSpeed: 300, // Speed back to top (ms)
	//        easingType: 'linear', // Scroll to top easing (see http://easings.net/)
	//        animation: 'fade', // Fade, slide, none
	//        animationSpeed: 200, // Animation in speed (ms)
	//        scrollTrigger: false, // Set a custom triggering element. Can be an HTML string or jQuery object
	//				//scrollTarget: false, // Set a custom target element for scrolling to the top
	//        scrollText: '<i class="fa fa-angle-up"></i>', // Text for element, can contain HTML
	//        scrollTitle: false, // Set a custom <a> title if required.
	//        scrollImg: false, // Set true to use image
	//        activeOverlay: false, // Set CSS color to display scrollUp active point, e.g '#00FFFF'
	//        zIndex: 2147483647 // Z-Index for the overlay
	//	});
	//});

    $('#datetimepicker1').datetimepicker({
        format: "YYYY-MM-DD hh:mm:ss",
        });
	$('[data-toggle="tooltip"]').tooltip();


	$(function () {
	    var $container = $('#Container'),
            $controls = $('.controls');

	    $container.mixItUp({
	        animation: {
	            duration: 700,
	            effects: 'fade translateY(600%) stagger(35ms)',
	            easing: 'cubic-bezier(0.86, 0, 0.07, 1)',
	            reverseOut: true
	        },
	        controls: {
	            enable: false
	        }
	    });

	    $controls.on('click', '.filter', function () {
	        var $btn = $(this),
                filter = $btn.attr('data-filter');

	        $btn.addClass('active').siblings().removeClass('active');

	        $container.mixItUp('filter', 'none', function () {
	            $container.mixItUp('filter', filter);
	        });
	    });

	});


});



