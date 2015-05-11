using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Backend
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //internal const string roleNombre = "Admin";

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
           
        }

        protected void Session_Start()
        {

            //var user = new ApplicationUser()
            //{
            //    UserName = "Admin",
            //    Email = "admin@admin.com",
            //    PasswordHash = "123456",
            //    Nombre = "Administrador",
            //    Apellido = "Administrador",
            //    FechaNacimiento = DateTime.Now,
            //    Imagen = "~/Imagenes/userdefault.png",
            //    Estado = true,
            //    Es_Admin = true
            //};
            
            //var user2 = new ApplicationUser() { UserName = "gbg933", SesionActual = Guid.NewGuid() };
         
        }
    }
}
