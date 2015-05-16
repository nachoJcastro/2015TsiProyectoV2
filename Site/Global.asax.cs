using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Site
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ingresarHosts();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
           

           // JobScheduler.Start();
        }

        private void ingresarHosts()
        {
            try 
	        {	
                AgregarHost("www");
                AgregarHost("");
                AgregarHost("sitio");
	            }
	            catch (Exception)
	            {
		
		            throw;
	            }
            
          
            
            throw new NotImplementedException();
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
