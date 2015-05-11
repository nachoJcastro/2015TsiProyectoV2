using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.App_Start
{
    public class HttpHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                context.Response.Write("  Prueba 1 ");
            }
            else
            {
                context.Response.Write("Prueba 2");
            }
        }
 
    }
}