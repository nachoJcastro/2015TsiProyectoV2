using Backend.Migrations;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Backend
{
    public class MvcApplication : System.Web.HttpApplication
    {
        internal const string roleNombre = "Admin";
        internal const string roleNombreUser = "Usuario";

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
           
        }

        protected void Session_Start()
        {

            IdentityManager manager = new IdentityManager();

            if (!manager.RoleExists(roleNombre))
            {

                manager.CreateRole(roleNombre);
                
                
                //var user = new ApplicationUser() { Email = "admin@chebay.com", UserName = "Administrador", Nombre = "Admin", Apellido = "Global" };

                var user = new ApplicationUser() { UserName = "admin@chebay.com"};

                if (manager.CreateUser(user, "Chebay2015"))
                {
                    manager.AddUserToRole(user.Id, roleNombre);

                }

            }

            if (!manager.RoleExists(roleNombreUser)) 
            {
                manager.CreateRole(roleNombreUser);
            }
        }
    }
}
