using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Site.App_Start;

namespace Site
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Agrega "subdominio" a los parametros de la ruta
            routes.Add(new RutaSubdominio());

            // ruta defecto
            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}",
               defaults: new { controller = "Home", action = "Index" }
           );

            // ruta para los tenants
            routes.MapRoute(
                name: "Tenant",
                url: "{controller}/{action}/{tenant}",
                defaults: new { controller = "Tenant", action = "Index", id = UrlParameter.Optional }
           );

           
        }
    }
}
