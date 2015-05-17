using Backend.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            ingresarHosts();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
           
        }

       /* protected void Session_Start()
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
         
        }*/

        private void ingresarHosts()
        {
            try
            {
                AgregarHost("www");
                AgregarHost("");
                AgregarHost("sitio");
                AgregarHost("backend");
            }
            catch (Exception)
            {

                throw;
            }




        }

        private void AgregarHost(string dominio)
        {
            var systemPath = Environment.GetFolderPath(Environment.SpecialFolder.System);
            var path = Path.Combine(systemPath, @"drivers\etc\hosts");
            using (var stream = new StreamWriter(path, true, Encoding.Default))
            {
                stream.WriteLine(@"127.0.0.1    " + dominio + ".chebay.com");
            }
        }
    }
}
