using System.Web;
using System.Web.Optimization;

namespace Site
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/modern-business").Include(
                       "~/Scripts/modern-business.js"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/knockout-3.3.0.js",
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.css",
                      "~/Content/flexslider.css",
                      "~/Content/site.css",
                      "~/Content/responsive.css",
                      "~/Content/price-range.css",
                      "~/Content/bootstrap-rating.css",
                      "~/Content/PagedList.css",
                      "~/Content/slick.css",
                      "~/Content/slick-theme.css",
                      "~/Content/bootstrap-datetimepicker.min.css"));


            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/moment.js",
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap-datetimepicker.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/jquery.zoom.min.js",
                      "~/Scripts/jquery.flexslider.js",
                      "~/Scripts/slick.js",
                      "~/Scripts/wow.min.js",
                      "~/Scripts/bootstrap-rating.js",
                      "~/Scripts/site.js",
                      "~/Scripts/main.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/dropzonescripts").Include(
                    "~/Scripts/dropzone/dropzone.min.js"));
            

            bundles.Add(new StyleBundle("~/Content/dropzonescss").Include(
                     "~/Scripts/dropzone/css/dropzone.css"));

        }
    }
}
