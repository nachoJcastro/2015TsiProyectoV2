using BusinessLogicLayer.TenantControllers;
using BusinessLogicLayer.TenantInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Site.App_Start
{
    class RutaSubdominio: RouteBase
    {
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            if (httpContext.Request == null || httpContext.Request.Url == null)
            {
                return null;
            }

            var host = httpContext.Request.Url.Host;
            System.Diagnostics.Debug.WriteLine(host.ToString());
            var index = host.IndexOf(".");
            string[] segmentos_URL = httpContext.Request.Url.PathAndQuery.TrimStart('/').Split('/');
            
            if (index < 0)
            {
                return null;
            }

            var subdominio = host.Substring(0, index);
            System.Diagnostics.Debug.WriteLine(subdominio.ToString());

            string[] lista_negra = { "backend", "www", "sitio","chebay" };
            IBLTenant _ibl = new BLTenant();
            _ibl.ExisteSitio(subdominio.ToString());

            if (lista_negra.Contains(subdominio))
            {
                return null;
            }

             System.Diagnostics.Debug.WriteLine("paso lista negra");
             var routeData = new RouteData(this, new MvcRouteHandler());
             string controlador = (segmentos_URL.Length > 0) ? segmentos_URL[0] : "Tenant";
             string accion = (segmentos_URL.Length > 1) ? segmentos_URL[1] : "Index";

             if (controlador.Length==0) controlador = "Tenant";
             if (accion.Length == 0) accion = "Index";

             System.Diagnostics.Debug.WriteLine("CONTROLADOR: "+ controlador + " ACCION " + accion + " SUBDOMINIO " + subdominio );

            if (_ibl.ExisteSitio(subdominio)){
                routeData.Values.Add("controller", controlador); // Va al controlador correspondiente
                routeData.Values.Add("action", accion); //Va a la accion correspondiente al controlador
                routeData.Values.Add("id", subdominio); //pasa el subdominio           
            }
            else{
                routeData.Values.Add("controller", "Tenant"); // Va al controlador correspondiente
                routeData.Values.Add("action", "Error"); //Va a la accion correspondiente al controlador
                routeData.Values.Add("id", subdominio.ToString()); //pasa el subdominio
            }
            return routeData;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            //Implement your formating Url formating here
            return null;
        }
    }
}
