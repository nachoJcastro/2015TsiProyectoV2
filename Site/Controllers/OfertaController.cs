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


        public ActionResult DetalleProducto(int idSubasta)
        {
            var user = Session["usuario"] as UsuarioSite;
            valor_tenant = user.Dominio;

            Subasta subasta = _blsubasta.ObtenerSubasta(valor_tenant,idSubasta);
            if (subasta == null)
            {
                return HttpNotFound();
            }
            return View(subasta);
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
        public ActionResult CreateOferta(int idSubasta)
        {
            idSub = idSubasta;
            ViewBag.idTienda = this.Session["_tiendaSesion"];
            return View();
        }
        // POST: Oferta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOferta([Bind(Include = "id,Monto")] Oferta oferta)//,id_Usuario,fecha
        {
            user = Session["usuario"] as UsuarioSite;
            valor_tenant = user.Dominio;
            oferta.id_Subasta = idSub;
            oferta.fecha = DateTime.UtcNow.Date;
            oferta.id_Usuario = _blusuario.ObtenerIdByEmail(valor_tenant, user.Email);

            _bloferta.AgregarOferta(valor_tenant, oferta);

            var listaOfer = _bloferta.ObtenerOfertasByProducto(valor_tenant, idSub);
            ViewBag.ListaOfertas = listaOfer;


            return View("Index");
        }


        //public ActionResult Create(string id)
        //{
        //    try
        //    {
        //        user = System.Web.HttpContext.Current.Session["usuario"] as UsuarioSite;
        //        Oferta oferta = new Oferta();
        //        subasta.id_Vendedor = 1;
        //        subasta.titulo = "Prueba";
        //        subasta.valor_Actual = 111;
        //        subasta.estado = EstadoTransaccion.Activa;
        //        subasta.finalizado = TipoFinalizacion.Subasta;
        //        user_sitio = Session["usuario"] as UsuarioSite;
        //        if (user_sitio.Dominio != null)
        //        {
        //            System.Diagnostics.Debug.WriteLine(" Dominio en sesion Login " + user_sitio.Dominio.ToString());
        //            valor_tenant = user_sitio.Dominio.ToString();
        //        }
        //        subIBL.AltaSubasta(valor_tenant, subasta);
        //        List<string> tipo = new List<string>();
        //        tipo.Add("Subasta");
        //        tipo.Add("Compra Directa");
        //        ViewData["Tipo"] = tipo;
        //        // ViewData["Categorias"] = proIBL.ObtenerCategoriasPorTienda(user.idTienda);
        //        // ViewData["Productos"] = proIBL.ObtenerTipoProdCategoria(user.idTienda);
        //        // ViewData["Atributos"] = proIBL.ObtenerAtributosTipoProd(user.idTienda);
        //        ViewBag.CategoriaId = new SelectList(proIBL.ObtenerCategoriasPorTienda(user.idTienda), "CategoriaId", "Nombre");
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return View();
        //}

    }
}