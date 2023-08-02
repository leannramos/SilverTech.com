using System.Web.Optimization;
using BundleTransformer.Core.Bundles;

namespace SitefinityWebApp.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new CustomStyleBundle("~/Content/core-css").Include(
                      "~/ResourcePackages/ST/FrontEnd/public/library/css/core.min.css"));

            bundles.Add(new CustomStyleBundle("~/Content/designer-css").Include(
                      "~/ResourcePackages/ST/FrontEnd/public/library/css/designer.min.css"));

            bundles.Add(new CustomScriptBundle("~/bundles/bankcore").Include(
                      "~/ResourcePackages/ST/library/js/vendor/svg4everybody.min.js",
                      "~/ResourcePackages/ST/library/js/vendor/tilt.jquery.min.js"));

            bundles.Add(new CustomScriptBundle("~/bundles/bankcore/live").Include(
                      "~/ResourcePackages/ST/library/js/vendor/bootstrap.min.js",
                      "~/ResourcePackages/ST/library/js/vendor/jquery.fancybox.min.js",
                      "~/ResourcePackages/ST/library/js/main.js"));

            bundles.Add(new CustomScriptBundle("~/bundles/main").Include(
                     "~/ResourcePackages/ST/FrontEnd/public/library/js/mainES6.js"));

            bundles.Add(new CustomScriptBundle("~/bundles/main-legacy").Include(
                     "~/ResourcePackages/ST/FrontEnd/public/library/js/main.js"));

            BundleTable.EnableOptimizations = true;
        }
    }
}