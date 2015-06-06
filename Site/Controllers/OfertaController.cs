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
        IBLSubasta _blsubasta;
        IBLUsuario _blusuario;
        private string valor_tenant;
        public UsuarioSite user;
        static int idSub;

        public OfertaController(IBLOferta bl, IBLSubasta blS, IBLUsuario blU)
        {
            this._bloferta = bl;
            this._blsubasta = blS;
            this._blusuario = blU;
        }

        public OfertaController() : this(new BLOferta(), new BLSubasta(), new BLUsuario())
        {

        }


        public ActionResult Lista(int idSubasta)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine(" Id subasta " + idSubasta.ToString());

                var user = Session["usuario"] as UsuarioSite;
                valor_tenant = user.Dominio;
                
                //int id_subasta = Convert.ToInt32(idSubasta);
                var lista = _bloferta.ObtenerOfertasByProducto(valor_tenant, idSubasta);
                List<OfertaModel> listOfer = new List<OfertaModel>();

                foreach (var item in lista)
                {
                    OfertaModel ofert = new OfertaModel();
                    ofert.id = item.id;
                    try
                    {
                        ofert.nombre = _blusuario.GetNombreUsuario(valor_tenant, item.id_Usuario);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    ofert.id_Subasta = item.id_Subasta;
                    ofert.id_Usuario = item.id_Usuario;
                    ofert.Monto = item.Monto;
                    ofert.fecha = item.fecha;

                    listOfer.Add(ofert);
                }
                ViewBag.ListaOfertas = listOfer;
                ViewBag.idSubasta = idSubasta;
            }
            catch (Exception)
            {
                throw;
            }
            return View();
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

            return View();
        }


        // GET: Oferta/Create
        public ActionResult CreateOferta(int idSubasta, int monto_actual)
        {
            idSub = idSubasta;
            ViewBag.idTienda = this.Session["_tiendaSesion"];
            ViewBag.idsub = idSubasta;
            ViewBag.monto = monto_actual;

            Oferta oferta = new Oferta();

            oferta.id_Subasta = idSubasta;
            oferta.Monto = monto_actual;
           
            return View(oferta);
        }


        // POST: Oferta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOferta([Bind(Include = "id,Monto")] Oferta oferta, double monto_actual)//,id_Usuario,fecha
        {
            var user = Session["usuario"] as UsuarioSite;
            valor_tenant = user.Dominio;

            var idOfertante = _blusuario.ObtenerIdByEmail(valor_tenant, user.Email);
            var ofertante = _blusuario.GetUsuario(valor_tenant, idOfertante);

            if (monto_actual>=oferta.Monto) {
                ModelState.AddModelError("", "La Oferta debe ser mayor a " +monto_actual);
                return View();
            }
            else if(ofertante.billetera < oferta.Monto){
                ModelState.AddModelError("", "Error usted solo dispone de $" + ofertante.billetera);
                return View();
            }
            else {
            
            user = Session["usuario"] as UsuarioSite;
            valor_tenant = user.Dominio;
            oferta.id_Subasta = idSub;
            
           
            oferta.fecha = DateTime.Now;
            oferta.id_Usuario = ofertante.id;

            _bloferta.AgregarOferta(valor_tenant, oferta);
            

            if (_blsubasta.ActualizarMonto(valor_tenant,oferta.id_Subasta, oferta.Monto))
            {
                var lista = _bloferta.ObtenerOfertasByProducto(valor_tenant, idSub);
                List<OfertaModel> listOfer = new  List<OfertaModel>();

                foreach (var item in lista)
                {
                    OfertaModel  ofert= new OfertaModel();
                    ofert.id= item.id;
                    try 
	                {	    
                       
		               ofert.nombre=_blusuario.GetNombreUsuario(valor_tenant,item.id_Usuario);
	                }
	                catch (Exception)
	                {
		
		                throw;
	                }

                    ofert.id_Subasta= item.id_Subasta;
                    ofert.id_Usuario= item.id_Usuario;
                    ofert.Monto= item.Monto;
                    ofert.fecha= item.fecha;

                    listOfer.Add(ofert);
                }

                try
                {
                    _bloferta.correoOferta(valor_tenant, oferta);

                }
                catch (Exception)
                {
                    
                    throw;
                }

                ViewBag.ListaOfertas = listOfer;
            }
           
            }
            return View("Index");
        }

    }

}