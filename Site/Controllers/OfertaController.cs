using BusinessLogicLayer.TenantControllers;
using BusinessLogicLayer.TenantInterfaces;
using Crosscutting.EntityTenant;
using Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Site.Controllers
{
    public class OfertaController : Controller
    {
        IBLOferta _bloferta;
        private UsuarioSite user_sitio;
        private string valor_tenant;

        public OfertaController(IBLOferta bl)
        {
            this._bloferta = bl;
        }

        public OfertaController() : this(new BLOferta())
        {

        }


        public ActionResult DetalleProducto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            else {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

           

            //return View("Error");
        }


        //// GET: Subastas/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Subasta subasta = db.Subastas.Find(id);
        //    if (subasta == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(subasta);
        //}





        // GET: Oferta
        public ActionResult Index()
        {
            int id_subasta = 0;//hay que conseguir el id subasta
            var listaOfer = _bloferta.ObtenerOfertasByProducto(id_subasta);
            ViewBag.ListaOfertas = listaOfer;

            user_sitio = Session["usuario"] as UsuarioSite;

            if (user_sitio.Dominio != null)
            {
                System.Diagnostics.Debug.WriteLine(" Dominio en sesion Login " + user_sitio.Dominio.ToString());
                valor_tenant = user_sitio.Dominio.ToString();
            }

            /*var lista_Subastas = _sub.ObtenerSubastas(valor_Tenant);
                ViewBag.ListarSubastas = lista_Subastas;*/

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