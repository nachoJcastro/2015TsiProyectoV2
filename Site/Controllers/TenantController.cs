using BusinessLogicLayer;
using BusinessLogicLayer.TenantControllers;
using BusinessLogicLayer.TenantInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Site.Controllers
{
    public class TenantController : Controller
    {
        IBLTenant _ibl = new BLTenant();

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
                return View();
            }
            else
            {
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