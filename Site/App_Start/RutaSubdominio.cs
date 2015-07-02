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
    class RutaSubdominio : RouteBase
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            if (httpContext.Request == null || httpContext.Request.Url == null)
            {
                return null;
            }

            var host = httpContext.Request.Url.Host;


            log.Warn("HOST " + host.ToString());
            log.Warn("URL PATH  " + httpContext.Request.Url.AbsolutePath);

            log.Warn("Absolute uri = " + httpContext.Request.Url.AbsoluteUri);
            var index = host.IndexOf(".");

            string seg_temp = httpContext.Request.Url.PathAndQuery.Replace("?", "/");

            log.Warn("SEGMENTO TEMPORAL= " + seg_temp);
            string[] segmentos_URL = seg_temp.TrimStart('/').Split('/');



            if (index < 0)
            {
                return null;
            }

            var subdominio = host.Substring(0, index);
            log.Warn(subdominio.ToString());

            string[] lista_negra = { "backend", "www", "sitio", "chebay" };
            IBLTenant _ibl = new BLTenant();
            _ibl.ExisteSitio(subdominio.ToString());

            if (lista_negra.Contains(subdominio))
            {
                return null;
            }

            log.Warn("paso lista negra");
            var routeData = new RouteData(this, new MvcRouteHandler());
            string controlador = (segmentos_URL.Length > 0) ? segmentos_URL[0] : "Tenant";
            string accion = (segmentos_URL.Length > 1) ? segmentos_URL[1] : "Index";
            string id = (segmentos_URL.Length > 2) ? segmentos_URL[2] : "";

            if (controlador.Length == 0) controlador = "Tenant";
            if (accion.Length == 0) accion = "Index";

            log.Warn("CONTROLADOR: " + controlador + " ACCION " + accion + " id " + id);

            if (_ibl.ExisteSitio(subdominio))
            {
                log.Warn("Controlador : " + controlador);
                log.Warn("Accion : " + accion);
                log.Warn("Id : " + id);




                routeData.Values.Add("controller", controlador); // Va al controlador correspondiente
                routeData.Values.Add("action", accion); //Va a la accion correspondiente al controlador

                if (controlador.Equals("Tenant") && accion.Equals("Index"))
                { routeData.Values.Add("id", subdominio); }
                foreach (var item in routeData.Values)
                {
                    log.Warn("ROUTE DATA IF VALUE : " + item.Value.ToString());
                }
            }
            else
            {
                routeData.Values.Add("controller", "Tenant"); // Va al controlador correspondiente
                routeData.Values.Add("action", "Error"); //Va a la accion correspondiente al controlador
                //routeData.Values.Add("id", subdominio); //pasa el subdominio
                //log.Warn("ROUTE DATA ELSE : " + routeData.Values.ToList().ToArray().ToString());
                foreach (var item in routeData.Values)
                {
                    log.Warn("ROUTE DATA ELSE VALUE : " + item.Value.ToString());
                }

            }
            log.Warn("RETORNO ROUTE data ");
            return routeData;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            //Implement your formating Url formating here
            return null;
        }
    }
}