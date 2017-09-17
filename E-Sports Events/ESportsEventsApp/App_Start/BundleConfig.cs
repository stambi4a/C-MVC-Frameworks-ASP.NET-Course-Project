using System.Web;
using System.Web.Optimization;

namespace ESportsEventsApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Content/scripts/jquery.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Content/scripts/modernizr-*"));

            bundles.Add(
                new ScriptBundle("~/bundles/bootstrap").Include(
                    //"~/Scripts/bootstrap.js",
                    "~/Content/scripts/bootstrap.min.js",
                    "~/Content/scripts/custom.js",
                    "~/Content/scripts/html5shiv.min.js",
                    //"~/Scripts/respond.js",
                    "~/Content/scripts/respond.min.js",
                    "~/Content/scripts/slick.min.js",
                    "~/Content/scripts/wow.min.js",
                    "~/Content/scripts/navigation.js"));


            bundles.Add(
                new StyleBundle("~/Content/css").Include(
                    //"~/Content/bootstrap.css",
                    "~/Content/animate.css",
                    "~/Content/bootstrap.min.css",
                    "~/Content/font-awesome.min.css",
                    "~/Content/slick.css",
                    "~/Content/style.css",
                    "~/Content/theme.css",
                    "~/Content/custom.css"
                    /*"~/Content/site.css"*/));

            bundles.Add(
                new ScriptBundle("~/bundles/ajax").Include(
                    "~/Content/scripts/MicrosoftAjax.js",
                    "~/Content/scripts/MicrosoftAjax.debugjs"));

            bundles.Add(
                new ScriptBundle("~/bundles/youtube").Include(
                    "~/Content/scripts/youtube.js"));
        }
    }
}
