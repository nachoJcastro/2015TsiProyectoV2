using BusinessLogicLayer;
using BusinessLogicLayer.TenantControllers;
using BusinessLogicLayer.TenantInterfaces;
using Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;
using System.Web.Mvc;

namespace Site.Controllers
{
    public class TenantController : Controller
    {
        IBLTenant _ibl = new BLTenant();
        IBLSubasta _sub = new BLSubasta();
        
        public UsuarioSite user;
        public string tenantID;
        /*static LocalDataStoreSlot local;
        private String tenantID;
        static LocalDataStoreSlot tenant = Thread.AllocateNamedDataSlot("Tenant");*/
        // GET: Tenant
        public ActionResult Index(string id)
        {
            tenantID = id;
            System.Diagnostics.Debug.WriteLine("ID tenant :" + tenantID);
            if (_ibl.ExisteSitio(tenantID))
            {
                /*System.Diagnostics.Debug.WriteLine("ID tenant :" + id);
                System.Diagnostics.Debug.WriteLine("Tenant");
                ViewBag.Message = id;
                Session["sitio"] = new Sitio { dominio =id };

                System.Threading.ThreadLocal<String> tenant;

               ThreadLocal<string> tenant_thread = new ThreadLocal<string>(() =>
                {
                    return id;
                });

                tenant_thread.Value.ToString(); */
                /*local = Thread.GetNamedDataSlot("tenant");
                if (local == null) { 
                
                    local = Thread.AllocateNamedDataSlot("tenant");
                }
                Thread.SetData(local, id);
                string valor_Tenant = System.Threading.Thread.GetData(local).ToString();*/
                if ( System.Web.HttpContext.Current.Session["usuario"] == null)
                    System.Web.HttpContext.Current.Session["usuario"] =new UsuarioSite();
                //if (user == null) Session["usuario"] = new UsuarioSite();
                //System.Web.HttpContext.Current.Session["usuario"] = user; 

                user = System.Web.HttpContext.Current.Session["usuario"] as UsuarioSite;
                user.Dominio = tenantID;
                user.idTienda = _ibl.ObtenerIdTenant(tenantID);
                System.Web.HttpContext.Current.Session["usuario"] = user;
                var lista_Subastas = _sub.ObtenerSubastas(tenantID);
                ViewBag.ListarSubastas = lista_Subastas;
                return View();
                //System.Diagnostics.Debug.WriteLine(" Dominio en sesion " + user.Dominio.ToString());
                //Thread t = Thread.CurrentThread;
                //System.Diagnostics.Debug.WriteLine(" Tenant Controller . desde el thread: "+ valor_Tenant);
            }
            else
            {
                Session.Clear();
                return RedirectToActionPermanent("Error");
            }

        }
        public ActionResult Error(string id)
        {
            ViewBag.Message = "Pagina de error. No existe el sitio: " + id;
            return View();


        }
    }
}