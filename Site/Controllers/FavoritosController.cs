using BusinessLogicLayer.TenantControllers;
using BusinessLogicLayer.TenantInterfaces;
using Crosscutting.EntityTenant;
using Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Site.Controllers
{
    public class FavoritosController : Controller
    {
        
        IBLSubasta subIBL;
        IBLUsuario usuIBL;
        IBLFavorito favIBL;

        BlobStorage _bls = new BlobStorage();
        public UsuarioSite user_sitio;
        private string valor_tenant;

        public FavoritosController(IBLSubasta subbl, IBLUsuario usubl, IBLFavorito favIBL)
        {
            this.subIBL = subbl;
            this.usuIBL = usubl;
            this.favIBL = favIBL;
        }

        public FavoritosController()
            : this(new BLSubasta(),  new BLUsuario(), new BLFavorito())
        {

        }

       
        // GET: Subastas
        public ActionResult Listar()
        {
            user_sitio = System.Web.HttpContext.Current.Session["usuario"] as UsuarioSite;
            valor_tenant = user_sitio.Dominio.ToString();
            int idLogueado = usuIBL.ObtenerIdByEmail(valor_tenant, user_sitio.Email);

            List<Subasta> favoritas = favIBL.SubastasFavoritasByUsuario(valor_tenant, idLogueado);
            ViewBag.favoritasLargo = favoritas.Count;
            ViewBag.favoritas = favoritas;

            return View();
            
        }







    }
}