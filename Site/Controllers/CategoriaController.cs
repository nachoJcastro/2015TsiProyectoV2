using BusinessLogicLayer.Controllers;
using BusinessLogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Site.Models;
using System.Threading;
using BusinessLogicLayer.TenantInterfaces;
using BusinessLogicLayer.TenantControllers;


namespace Site.Controllers
{
    public class CategoriaController : Controller
    {
        IBLCategoria _blcategoria;
        IBLTenant _bltenant;
        public string valor_tenant;
        static LocalDataStoreSlot local = null;

        public CategoriaController(IBLCategoria bl, IBLTenant blT)
        {
            this._blcategoria = bl;
            this._bltenant = blT;
        }

        public CategoriaController() : this(new BLCategoria(), new BLTenant())
        {

        }


        // GET: Producto
        public ActionResult Index()
        {
            //System.Threading.Thread.Sleep(3000);
            //local = Thread.GetNamedDataSlot("tenant");
            //string valor_Tenant = System.Threading.Thread.GetData(local).ToString();
            try
            {
                var user = Session["usuario"] as UsuarioSite;
                valor_tenant = user.Dominio;

                if (valor_tenant == null) System.Diagnostics.Debug.WriteLine("tenant nulo");
                System.Diagnostics.Debug.WriteLine(valor_tenant);
                var idTenant = _bltenant.ObtenerIdTenant(valor_tenant);
                var lista_Categorias = _blcategoria.ObtenerCategoriasPorTienda(idTenant);
                ViewBag.ListaCategorias = lista_Categorias;

            }
            catch (Exception)
            {
                
                throw;
            }

            

            return View();
        }
    }
}