using Backend.Migrations;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        //internal const string roleNombreUser = "Usuario";

        protected void Application_Start()
        {
           // ingresarHosts();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());

        }

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


        //protected void Session_Start()
        //{

        //    IdentityManager manager = new IdentityManager();

        //    if (!manager.RoleExists(roleNombre))
        //    {

        //        manager.CreateRole(roleNombre);
        //        manager.CreateRole(roleNombreUser);

        //        var user = new ApplicationUser() { Email = "admin@chebay.com", UserName = "Administrador", Nombre = "Admin", Apellido = "Global"};

        //        if (manager.CreateUser(user, "Chebay2015"))
        //        {
        //            manager.AddUserToRole(user.Id, roleNombre);

        //        }

        //    }
        //}
    }
}