/* globals hopscotch: false */

/* ============ */
/* EXAMPLE TOUR */
/* ============ */
var tour = {
    id: 'hello-hopscotch',
    steps: [
      {
          target: 'sub_img',
          title: 'Subir Imagenes!',
          content: 'Las imagenes que se suban en esta seccion se mostraran en el slider principal de la tienda virtual.',
          placement: 'top',
          xOffset: 0,
          yOffset: 0,
      },
      {
          //target: document.querySelectorAll('#general-use-desc code')[1],
          target:'edit_tienda',
          title: 'Editar Tienda Virtual',
          content: 'Se podran editar datos basicos de la tienda, como descripción, logo, etc.',
          placement: 'right',
          xOffset: -15,
          yOffset: -10,
      },
      {
          target: 'editor_css',
          placement: 'top',
          title: 'Editar estilo',
          content: 'En esta seccion se encuentra un editor con todo el css de la tienda virtual para poder modificarlo.',
          xOffset: 0,
          yOffset: 0,
      },
      {
          target: 'comp_tienda',
          placement: 'right',
          title: 'Componentes',
          content: 'Los componentes de la tienda virtual son sus categorias, atributos y tipo de productos.',
          xOffset: 0,
          yOffset: -10,
      },
      {
          target: 'elim_tienda',
          placement: 'top',
          title: 'Eliminar tienda',
          content: 'En caso de no ser satisfactorio el producto se da la opcion de eliminar la tienda y volver a crear una nueva a gusto!',
          xOffset:-25,
          yOffset:10,
      }
    ],
    showPrevButton: true,
    scrollTopMargin: 100
},

/* ========== */
/* TOUR SETUP */
/* ========== */
addClickListener = function (el, fn) {
    if (el.addEventListener) {
        el.addEventListener('click', fn, false);
    }
    else {
        el.attachEvent('onclick', fn);
    }
},

init = function () {
    var startBtnId = 'startTourBtn',
        calloutId = 'startTourCallout',
        mgr = hopscotch.getCalloutManager(),
        state = hopscotch.getState();

    if (state && state.indexOf('hello-hopscotch:') === 0) {
        // Already started the tour at some point!
        hopscotch.startTour(tour);
    }
    else {
        // Looking at the page for the first(?) time.
        setTimeout(function () {
            mgr.createCallout({
                id: calloutId,
                target: startBtnId,
                placement: 'right',
                title: 'Guia de Uso',
                content: 'Inicie la guia para aprender a usar Che-Bay!',
                yOffset: -25,
                arrowOffset: 20,
                width: 240
            });
        }, 100);
    }

    addClickListener(document.getElementById(startBtnId), function () {
        if (!hopscotch.isActive) {
            mgr.removeAllCallouts();
            hopscotch.startTour(tour);
        }
    });
};

init();