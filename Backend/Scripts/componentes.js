$(document).ready(function () {

    /*------------- CATEGORIAS -------------*/
    $('#btnCategoria').click(function () {
        $(".modal-dialog").hide();
        $('.modal-body').load("/componentes/createcategoria");
        setTimeout(function () {
            $(".modal-dialog").fadeIn();
        }, 100);
    });

    $('.btnDeleteCat').click(function () {
        $(".modal-dialog").hide();
        $('.modal-body').load("/componentes/deletecategoria/" + $(this).data("id"));
        setTimeout(function () {
            $(".modal-dialog").fadeIn();
        }, 100);
    });

    $('.btnEditarCat').click(function () {
        $(".modal-dialog").hide();
        $('.modal-body').load("/componentes/editcategoria/" + $(this).data("id"));
        setTimeout(function () {
            $(".modal-dialog").fadeIn();
        }, 100);

    });

    /*------------- T. PRODUCTO -------------*/
    $('#btnTiposP').click(function () {
        $(".modal-dialog").hide();
        $('.modal-body').load("/componentes/createtipoproducto");

        setTimeout(function () {
            $(".modal-dialog").fadeIn();
        }, 100);
    });

    $('.btnDeleteT').click(function () {
        $(".modal-dialog").hide();
        $('.modal-body').load("/componentes/deletetipoproducto/" + $(this).data("id"));

        setTimeout(function () {
            $(".modal-dialog").fadeIn();

        }, 100);
    });

    $('.btnEditarT').click(function () {
        $(".modal-dialog").hide();
        $('.modal-body').load("/componentes/edittipoproducto/" + $(this).data("id"));
        setTimeout(function () {
            $(".modal-dialog").fadeIn();

        }, 100);
        //$('.modal-body').load("/componente/editartecnologia/" + $(this).data("id"));

    });

    /*------------- ATRIBUTOS -------------*/
    $('#btnAtributos').click(function () {
        $(".modal-dialog").hide();
        $('.modal-body').load("/componentes/createatributo");

        setTimeout(function () {
            $(".modal-dialog").fadeIn();
        }, 100);
    });

    $('.btnDeleteA').click(function () {
        $(".modal-dialog").hide();
        $('.modal-body').load("/componentes/deleteatributo/" + $(this).data("id"));

        setTimeout(function () {
            $(".modal-dialog").fadeIn();
        }, 100);
    });

    $('.btnEditarA').click(function () {
        $(".modal-dialog").hide();
        $('.modal-body').load("/componentes/editatributo/" + $(this).data("id"));

        setTimeout(function () {
            $(".modal-dialog").fadeIn();
        }, 100);
    });

    /*------------- IMAGENES -------------*/
    $('#btnImg').click(function () {
        $(".modal-dialog").hide();
        $('.modal-body').load("/componentes/createimagen");

        setTimeout(function () {
            $(".modal-dialog").fadeIn();
        }, 100);
    });

    $('.btnDeleteI').click(function () {
        $(".modal-dialog").hide();
        $('.modal-body').load("/componentes/deleteimagen/" + $(this).data("id"));
        setTimeout(function () {
            $(".modal-dialog").fadeIn();
        }, 100);
    });

    $('.btnEditarI').click(function () {
        $(".modal-dialog").hide();
        $('.modal-body').load("/componentes/editimagen/" + $(this).data("id"));

        setTimeout(function () {
            $(".modal-dialog").fadeIn();
        }, 100);
    });




    /*------------- TIENDA INDEX -------------*/

    $('.btnDeleteTienda').click(function () {
        $(".modal-dialog").hide();
        $('.modal-body').load("/tiendavirtual/delete/" + $(this).data("id"));

        setTimeout(function () {
            $(".modal-dialog").fadeIn();
        }, 100);
    });

    $('#btnTiendaV').click(function () {
        $(".modal-dialog").hide();
        $('.modal-body').load("/tiendavirtual/create");

        setTimeout(function () {
            $(".modal-dialog").fadeIn();
        }, 100);
    });

    $('.btnEditTienda').click(function () {
        $(".modal-dialog").hide();
        $('.modal-body').load("/tiendavirtual/edit/" + $(this).data("id"));

        setTimeout(function () {
            $(".modal-dialog").fadeIn();
        }, 100);
    });

    
    /*$('.btnEliminarJuegoAdmin').click(function () {
        $(".modal-dialog").hide();
        $("#loading").fadeIn();
        $('.modal-body').load("/juego/delete/" + $(this).data("id"));
        var opts = {
            lines: 12, // The number of lines to draw
            length: 7, // The length of each line
            width: 4, // The line thickness
            radius: 10, // The radius of the inner circle
            color: '#000', // #rgb or #rrggbb
            speed: 1, // Rounds per second
            trail: 60, // Afterglow percentage
            shadow: false, // Whether to render a shadow
            hwaccel: false // Whether to use hardware acceleration
        };
        var target = document.getElementById('loading');
        var spinner = new Spinner(opts).spin(target);

        setTimeout(function () {
            $("#loading").fadeOut(1000);
            $(".modal-dialog").slideToggle();

        }, 3000);
    });
    
    //MIS JUEGOS
    $('.btnEditarMisJuegos').click(function () {
        $(".modal-dialog").hide();
        $("#loading").fadeIn();
        $('.modal-body').load("/juego/edit/" + $(this).data("id"));
        var opts = {
            lines: 12, // The number of lines to draw
            length: 7, // The length of each line
            width: 4, // The line thickness
            radius: 10, // The radius of the inner circle
            color: '#000', // #rgb or #rrggbb
            speed: 1, // Rounds per second
            trail: 60, // Afterglow percentage
            shadow: false, // Whether to render a shadow
            hwaccel: false // Whether to use hardware acceleration
        };
        var target = document.getElementById('loading');
        var spinner = new Spinner(opts).spin(target);

        setTimeout(function () {
            $("#loading").fadeOut(1000);
            $(".modal-dialog").slideToggle();

        }, 3000);
    });
    */

});