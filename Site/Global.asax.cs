using log4net;
using Quartz;
using Site.Tareas;
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

        public static IScheduler scheduler;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        protected void Application_Start()
        {
           
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // CHAT 
            //RouteTable.Routes.MapHubs();
            // QUARTZ.NET TAREAS
            log.Warn("ANTES DE SchedulerSubasta");
            //SchedulerMail.Start();
            SchedulerSubasta.Start();
            log.Warn("DESPUES DE SchedulerSubasta");
           
        }

       
    }
}
