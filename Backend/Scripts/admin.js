/*---LEFT BAR ACCORDION----*/
$(function() {
    $('#nav-accordion2').dcAccordion({
        eventType: 'click',
        autoClose: true,
        saveState: true,
        disableLink: true,
        speed: 'slow',
        showCount: false,
        autoExpand: true,
//        cookie: 'dcjq-accordion-1',
        classExpand: 'dcjq-current-parent'
    });


});

$(function() {
    $('#nav-accordion').dcAccordion({
        eventType: 'click',
        autoClose: true,
        saveState: true,
        disableLink: true,
        speed: 'slow',
        showCount: false,
        autoExpand: true,
//        cookie: 'dcjq-accordion-1',
        classExpand: 'dcjq-current-parent'
    });

});

$(function() {
  var activoHeader = 50;
  $(window).scroll(function() {
    var scroll = getCurrentScroll();
      if ( scroll >= activoHeader ) {
           $('.logo-index').addClass('activo');
           $('.menu-index').addClass('activo');
           $('.navbar-chebay').addClass('activo');

        }
        else {
            $('.logo-index').removeClass('activo');
            $('.menu-index').removeClass('activo');
            $('.navbar-chebay').removeClass('activo');

        }
  });
  function getCurrentScroll() {
    return window.pageYOffset || document.documentElement.scrollTop;
  }

});

var Script = function () {


//    sidebar toggle

    $(function() {
        function responsiveView() {
            var wSize = $(window).width();
            if (wSize <= 768) {
                $('#container').addClass('sidebar-close');
                $('#sidebar > ul').hide();
            }

            if (wSize > 768) {
                $('#container').removeClass('sidebar-close');
                $('#sidebar > ul').show();
            }
        }
        $(window).on('load', responsiveView);
        $(window).on('resize', responsiveView);
    });

    $('.fa-bars').click(function () {
        if ($('#sidebar > ul').is(":visible") === true) {
            //$('#main-content').css({
            //    'margin-left': '0px'
            //});
            $('#sidebar').css({
                'margin-left': '-250px'
            });
            $('.main-content').css({
                'margin-left': '0px'
            });
            $('#sidebar > ul').hide();
            //$("#container").addClass("sidebar-closed");
        } else {
            //$('#main-content').css({
            //    'margin-left': '250px'
            //});
            $('#sidebar > ul').show();
            $('#sidebar').css({
                'margin-left': '0px'
            });
            $('.main-content').css({
                'margin-left': '250px'
            });
            //$("#container").removeClass("sidebar-closed");
        }
    });

// custom scrollbar
    $("#sidebar").niceScroll({styler:"fb",cursorcolor:"#4ECDC4", cursorwidth: '3', cursorborderradius: '10px', background: '#404040', spacebarenabled:false, cursorborder: ''});

    $("html").niceScroll({styler:"fb",cursorcolor:"#4ECDC4", cursorwidth: '6', cursorborderradius: '10px', background: '#404040', spacebarenabled:false,  cursorborder: '', zindex: '1000'});
    $("#site").niceScroll({styler:"fb",cursorcolor:"#E22929", cursorwidth: '6', cursorborderradius: '10px', background: '#404040', spacebarenabled:false,  cursorborder: '', zindex: '1000'});

    wow = new WOW(
      {
          boxClass: 'wow',      // default
          animateClass: 'animated', // default
          offset: 0,          // default
          mobile: true,       // default
          live: true        // default
      }
    )
    wow.init();

}();
