using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Backend.App_Start
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
            var index = host.IndexOf(".");
            string[] segmentos_URL = httpContext.Request.Url.PathAndQuery.TrimStart('/').Split('/');

            if (index < 0)
            {
                return null;
            }

            var subdominio = host.Substring(0, index);
            string[] lista = { "backend", "www"};

            if (!lista.Contains(subdominio))
            {
                return null;
            }

            string controlador = (segmentos_URL.Length > 0) ? segmentos_URL[0] : "Home";
            string accion = (segmentos_URL.Length > 1) ? segmentos_URL[1] : "Index";

            var routeData = new RouteData(this, new MvcRouteHandler());
            routeData.Values.Add("controller", controlador); // Va al controlador correspondiente
            routeData.Values.Add("action", accion); //Va a la accion correspondiente al controlador
            //routeData.Values.Add("subdomain", subdominio); //pasa el subdominio
            return routeData;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            //Implement your formating Url formating here
            return null;
        }
    }
}
