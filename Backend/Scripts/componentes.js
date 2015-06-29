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
        var id = $(this).data("id");
        if (id < 1) {

            $(".modal-dialog").hide();
            $('.modal-body').load("/tiendavirtual/create");

            setTimeout(function () {
                $(".modal-dialog").fadeIn();
            }, 100);
        }
        else {
            mensaje("Tienda Virtual", "Solo se permite crear una tienda virtual por usuario", "error");
        }
    });

    $('.btnEditTienda').click(function () {
        $(".modal-dialog").hide();
        $('.modal-body').load("/tiendavirtual/edit/" + $(this).data("id"));

        setTimeout(function () {
            $(".modal-dialog").fadeIn();
        }, 100);
    });

   
  

});

function mensaje(titulo, texto, tipo) {
    swal({
        title: titulo,
        text: texto,
        type: tipo,
    });
}