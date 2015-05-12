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
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/otros").Include(
                      "~/Scripts/codemirror-2.37/lib/codemirror.js",
                      "~/Scripts/codemirror-2.37/mode/css/css.js",
                      "~/Scripts/codemirror-2.37/addon/hint/show-hint.js",
                      "~/Scripts/codemirror-2.37/addon/hint/css-hint.js",
                      "~/Scripts/editor_conf.js"));

            bundles.Add(new ScriptBundle("~/bundles/editor").Include(
                      "~/Scripts/nifty.min.js",
                      "~/Scripts/jquery.backstretch.min.js",
                      "~/Scripts/jquery.dcjqaccordion.js",
                      "~/Scripts/jquery.nicescroll.js",
                      "~/Scripts/wow.min.js",
                      "~/Scripts/admin.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/animate.css",
                      "~/Content/nifty.min.css",
                      "~/Content/font-awesome.css",
                      "~/Content/estilo.css"));

            bundles.Add(new StyleBundle("~/Content/editor").Include(
                      "~/Scripts/codemirror-2.37/lib/codemirror.css",
                      "~/Scripts/codemirror-2.37/theme/monokai.css"));
        }
    }
}
