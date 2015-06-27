/*---LEFT BAR ACCORDION----*/
$(function () {
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

$(function () {
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

$(function () {
    var activoHeader = 50;
    $(window).scroll(function () {
        var scroll = getCurrentScroll();
        if (scroll >= activoHeader) {
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
    $(function () {
        function responsiveView() {
            var wSize = $(window).width();
        
            if (wSize <= 768) {
                $('#collapse-2').hide();
                $('#sidebar > ul').hide();
            }

            if (wSize > 768) {
                $('#collapse-2').show();
                $('#sidebar > ul').show();
            }
        }
        $(window).on('load', responsiveView);
        $(window).on('resize', responsiveView);
    });

    $('#juas').click(function () {

        if ($('#collapse-2').is(":visible") === true) {
            $('#sidebar > ul').hide();
            $("#collapse-2").hide();
        } else {

            $('#sidebar > ul').show();
            $("#collapse-2").show();
        }
    });

    $('[data-toggle="tooltip"]').tooltip();
    // custom scrollbar
    $("#sidebar").niceScroll({ styler: "fb", cursorcolor: "#4ECDC4", cursorwidth: '3', cursorborderradius: '10px', background: '#404040', spacebarenabled: false, cursorborder: '' });

    $("html").niceScroll({ styler: "fb", cursorcolor: "#4ECDC4", cursorwidth: '6', cursorborderradius: '10px', background: '#404040', spacebarenabled: false, cursorborder: '', zindex: '1000' });
   

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


    //$('#datetimepicker1').datetimepicker({
    //    format: "YYYY-MM-DD hh:mm:ss",
    //});

    $('#datetimepicker2').datetimepicker({
        format: "YYYY-MM-DD hh:mm:ss",
    });

    $('#datetimepicker3').datetimepicker({
        format: "YYYY-MM-DD hh:mm:ss",
    });

    $('#datetimepicker4').datetimepicker({
        format: "YYYY-MM-DD hh:mm:ss",
    });

    $('#datetimepicker5').datetimepicker({
        format: "YYYY-MM-DD hh:mm:ss",
    });

    $('#datetimepicker6').datetimepicker({
        format: "YYYY-MM-DD hh:mm:ss",
    });

    $('#datetimepicker7').datetimepicker({
        format: "YYYY-MM-DD hh:mm:ss",
    });
    $('#datetimepicker8').datetimepicker({
        format: "YYYY-MM-DD hh:mm:ss",
    });
    $('#datetimepicker9').datetimepicker({
        format: "YYYY-MM-DD hh:mm:ss",
    });
    $('#datetimepicker10').datetimepicker({
        format: "YYYY-MM-DD hh:mm:ss",
    });
}();