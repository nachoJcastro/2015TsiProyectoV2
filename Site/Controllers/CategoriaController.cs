using BusinessLogicLayer.TenantControllers;
using BusinessLogicLayer.TenantInterfaces;
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
    public class CategoriaController : Controller
    {
        IBLCategoria _blcategoria;
        private string valor_tenant;
        static LocalDataStoreSlot local = null;

        public CategoriaController(IBLCategoria bl)
        {
            this._blcategoria = bl;
        }

        public CategoriaController() : this(new BLCategoria())
        {

        }


        // GET: Producto
        public ActionResult Index()
        {
            System.Threading.Thread.Sleep(3000);
            local = Thread.GetNamedDataSlot("tenant");
            string valor_Tenant = System.Threading.Thread.GetData(local).ToString();
            var user = Session["usuario"] as UsuarioSite;

            var lista_Categorias = _blcategoria.ObtenerCategorias(valor_Tenant);
            ViewBag.ListaCategorias = lista_Categorias;

            return View();
        }
    }
}