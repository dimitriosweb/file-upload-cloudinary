using System.Web;
using System.Web.Optimization;

namespace EFStories
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryupload").Include(
                        "~/Scripts/cloudinary/jquery.ui.widget.js",
                        "~/Scripts/cloudinary/jquery.iframe-transport.js",
                        "~/Scripts/cloudinary/jquery.fileupload.js",
                        "~/Scripts/cloudinary/jquery.cloudinary.js",
                        "~/Scripts/cloudinary/canvas-to-blob.min.js",
                        "~/Scripts/cloudinary/jquery.fileupload-image.js",
                        "~/Scripts/cloudinary/jquery.fileupload-process.js ",
                        "~/Scripts/cloudinary/jquery.fileupload-validate.js",
                        "~/Scripts/cloudinary/load-image.all.min.js"
                        ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}









