using System.Web;
using System.Web.Optimization;

namespace Backend
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery.js"));
            //"~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap-datetimepicker.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/otros").Include(
                      "~/Scripts/nifty.min.js",
                      "~/Scripts/jquery.backstretch.min.js",
                      "~/Scripts/jquery.dcjqaccordion.js",
                      "~/Scripts/jquery.nicescroll.js",
                      "~/Scripts/wow.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/admin").Include(
                        "~/Scripts/admin.js"));

            bundles.Add(new ScriptBundle("~/bundles/root").Include(
                       "~/Scripts/root.js"));

            bundles.Add(new ScriptBundle("~/bundles/chart").Include(
                      "~/Scripts/exporting.js",
                      "~/Scripts/highchart.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Scripts/moment.js",
                      "~/Content/bootstrap.css",
                      "~/Content/animate.css",
                      "~/Content/nifty.min.css",
                      "~/Content/font-awesome.css",
                      "~/Content/estilo.css",
                      "~/Content/bootstrap-datetimepicker.min.css"));

        }
    }
}
