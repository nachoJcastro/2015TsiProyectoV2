using BusinessLogicLayer;
using BusinessLogicLayer.TenantControllers;
using BusinessLogicLayer.TenantInterfaces;
using Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Site.Controllers
{
    public class TenantController : Controller
    {
        IBLTenant _ibl = new BLTenant();
        //[ThreadStatic]
        static LocalDataStoreSlot local;

        /* private String tenantID;
        static LocalDataStoreSlot tenant = Thread.AllocateNamedDataSlot("Tenant");*/
        // GET: Tenant
        public ActionResult Index(string id)
        {

            if (_ibl.ExisteSitio(id))
            {
                System.Diagnostics.Debug.WriteLine("ID tenant :" + id);
                System.Diagnostics.Debug.WriteLine("Tenant");
                ViewBag.Message = " Entre al Tenant : " + id;
                //Session["sitio"] = new Sitio { dominio =id };

                //System.Threading.ThreadLocal<String> tenant;

               /* ThreadLocal<string> tenant_thread = new ThreadLocal<string>(() =>
                {
                    return id;
                });

                tenant_thread.Value.ToString(); */
                local = Thread.GetNamedDataSlot("tenant");
                if (local == null) { 
                
                    local = Thread.AllocateNamedDataSlot("tenant");
                }
                Thread.SetData(local, id);
                string valor_Tenant = System.Threading.Thread.GetData(local).ToString();

                var user = Session["usuario"] as UsuarioSite;

                if (user == null) Session["usuario"] = new UsuarioSite { Dominio = id };

                user = Session["usuario"] as UsuarioSite;
                System.Diagnostics.Debug.WriteLine(" Dominio en sesion " + user.Dominio.ToString());
                //Thread t = Thread.CurrentThread;
                System.Diagnostics.Debug.WriteLine(" Tenant Controller . desde el thread: "+ valor_Tenant);
                return View();
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