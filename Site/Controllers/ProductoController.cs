using BusinessLogicLayer.TenantControllers;
using BusinessLogicLayer.TenantInterfaces;
using Crosscutting.EntityTenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Site.Models;
using System.Threading;

namespace Site.Controllers
{
    public class ProductoController : Controller
    {
        IBLProducto _blproducto;
        private UsuarioSite user_sitio;
        private string valor_tenant;
        static LocalDataStoreSlot local;

        public ProductoController(IBLProducto bl)
        {
            this._blproducto = bl;
        }

        public ProductoController() : this(new BLProducto())
        {

        }


        // GET: Producto
        public ActionResult Index()
        {
            local = Thread.GetNamedDataSlot("tenant");
            string valor_Tenant = System.Threading.Thread.GetData(local).ToString();

            var user = Session["usuario"] as UsuarioSite;

            var lista_Productos = _blproducto.ObtenerProductos();
            ViewBag.ListaCategorias = lista_Productos;

            return View();
        }


        // GET: Producto/Create
        public ActionResult CreateProducto()
        {
            ViewBag.idTienda = this.Session["_tiendaSesion"];
            return View();
        }

        // POST: Producto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProducto([Bind(Include = "")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                //producto.TiendaId = Int32.Parse(this.Session["_tiendaSesion"].ToString());
                _blproducto.AgregarProducto(producto);
                return RedirectToAction("Index", new { id = this.Session["_tiendaSesion"] });
            }
            return View(producto);
        }

    }
}