using BusinessLogicLayer.TenantControllers;
using BusinessLogicLayer.TenantInterfaces;
using Crosscutting.EntityTenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Site.Controllers
{
    public class OfertaController : Controller
    {
        IBLOferta _bloferta;

        public OfertaController(IBLOferta bl)
        {
            this._bloferta = bl;
        }

        public OfertaController() : this(new BLOferta())
        {

        }


        // GET: Oferta
        public ActionResult Index(int id)
        {
            int id_subasta = 0;//hay que conseguir el id subasta
            var listaOfer = _bloferta.ObtenerOfertasByProducto(id_subasta);
            ViewBag.ListaOfertas = listaOfer;

            this.Session["_tiendaSesion"] = id;
            ViewBag.idT = this.Session["_tiendaSesion"];

            return View();
        }


        #region Ofertas

        // GET: Oferta/Create
        public ActionResult CreateOferta()
        {
            ViewBag.idTienda = this.Session["_tiendaSesion"];
            return View();
        }

        // POST: Oferta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOferta([Bind(Include = "id,Monto,id_Usuario,fecha,id_Subasta")] Oferta oferta)
        {
            if (ModelState.IsValid)
            {
                //oferta.TiendaId = Int32.Parse(this.Session["_tiendaSesion"].ToString());
                _bloferta.AgregarOferta(oferta);
                return RedirectToAction("Index", new { id = this.Session["_tiendaSesion"] });
            }

            return View(oferta);
        }

        #endregion

    }
}