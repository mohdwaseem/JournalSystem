using System.Web;
using System.Web.Optimization;

namespace JournalSystem
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/base").Include(
                        "~/Scripts/jquery-2.1.4.js",
                         "~/plugins/jquery-ui/jquery-ui-1.10.2.custom.min.js",
                         "~/plugins/touchpunch/jquery.ui.touch-punch.min.js",
                         "~/bootstrap/js/bootstrap.min.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/plugins").Include(

                         "~/assets/js/libs/lodash.compat.min.js",
                         "~/plugins/event.swipe/jquery.event.move.js",
                         "~/plugins/event.swipe/jquery.event.swipe.js",
                         "~/assets/js/libs/breakpoints.js",
                         "~/plugins/respond/respond.min.js",
                         "~/plugins/cookie/jquery.cookie.min.js",
                         "~/plugins/slimscroll/jquery.slimscroll.min.js",
                         "~/plugins/slimscroll/jquery.slimscroll.horizontal.min.js",
                         "~/plugins/sparkline/jquery.sparkline.min.js",
                         "~/plugins/flot/jquery.flot.min.js",
                         "~/plugins/flot/jquery.flot.tooltip.min.js",
                         "~/plugins/flot/jquery.flot.resize.min.js",
                         "~/plugins/flot/jquery.flot.time.min.js",
                         "~/plugins/flot/jquery.flot.growraf.min.js",
                         "~/plugins/easy-pie-chart/jquery.easy-pie-chart.min.js",
                         "~/plugins/daterangepicker/moment.min.js",
                         "~/plugins/daterangepicker/daterangepicker.js",
                         "~/plugins/blockui/jquery.blockUI.min.js",
                         "~/plugins/fullcalendar/fullcalendar.min.js",
                         "~/plugins/noty/jquery.noty.js",
                         "~/plugins/noty/layouts/top.js",
                         "~/plugins/noty/themes/default.js",
                         "~/plugins/uniform/jquery.uniform.min.js",
                         "~/plugins/select2/select2.min.js",
                         "~/assets/js/app.js",
                         "~/assets/js/plugins.js",
                         "~/plugins/pickadate/picker.js",
                         "~/plugins/pickadate/picker.date.js",
                         "~/plugins/pickadate/picker.time.js",
                         "~/assets/js/plugins.form-components.js",
                         "~/assets/js/custom.js",
                         "~/assets/js/demo/pages_calendar.js",
                         "~/assets/js/demo/charts/chart_filled_blue.js",
                         "~/assets/js/demo/charts/chart_simple.js"
                        ));



            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/bootstrap/css/bootstrap.min.css",
                      "~/assets/css/main.css",
                      "~/assets/css/responsive.css",
                      "~/assets/css/icons.css",
                      "~/assets/css/plugins/jquery-ui.css"
                      ));
            bundles.Add(new StyleBundle("~/Content/jQuery-File-Upload").Include(
                    "~/Content/jQuery.FileUpload/css/jquery.fileupload.css",
                   "~/Content/jQuery.FileUpload/css/jquery.fileupload-ui.css",
                   "~/Content/blueimp-gallery2/css/blueimp-gallery.css",
                     "~/Content/blueimp-gallery2/css/blueimp-gallery-video.css",
                       "~/Content/blueimp-gallery2/css/blueimp-gallery-indicator.css"
                   ));

            bundles.Add(new ScriptBundle("~/bundles/jQuery-File-Upload").Include(
                     //<!-- The Templates plugin is included to render the upload/download listings -->
                     "~/Scripts/jQuery.FileUpload/vendor/jquery.ui.widget.js",
                       "~/Scripts/jQuery.FileUpload/tmpl.min.js",
//<!-- The Load Image plugin is included for the preview images and image resizing functionality -->
"~/Scripts/jQuery.FileUpload/load-image.all.min.js",
//<!-- The Canvas to Blob plugin is included for image resizing functionality -->
"~/Scripts/jQuery.FileUpload/canvas-to-blob.min.js",
//"~/Scripts/file-upload/jquery.blueimp-gallery.min.js",
//<!-- The Iframe Transport is required for browsers without support for XHR file uploads -->
"~/Scripts/jQuery.FileUpload/jquery.iframe-transport.js",
//<!-- The basic File Upload plugin -->
"~/Scripts/jQuery.FileUpload/jquery.fileupload.js",
//<!-- The File Upload processing plugin -->
"~/Scripts/jQuery.FileUpload/jquery.fileupload-process.js",
//<!-- The File Upload image preview & resize plugin -->
"~/Scripts/jQuery.FileUpload/jquery.fileupload-image.js",
//<!-- The File Upload audio preview plugin -->
"~/Scripts/jQuery.FileUpload/jquery.fileupload-audio.js",
//<!-- The File Upload video preview plugin -->
"~/Scripts/jQuery.FileUpload/jquery.fileupload-video.js",
//<!-- The File Upload validation plugin -->
"~/Scripts/jQuery.FileUpload/jquery.fileupload-validate.js",
//!-- The File Upload user interface plugin -->
"~/Scripts/jQuery.FileUpload/jquery.fileupload-ui.js",
//Blueimp Gallery 2 
"~/Scripts/blueimp-gallery2/js/blueimp-gallery.js",
"~/Scripts/blueimp-gallery2/js/blueimp-gallery-video.js",
"~/Scripts/blueimp-gallery2/js/blueimp-gallery-indicator.js",
"~/Scripts/blueimp-gallery2/js/jquery.blueimp-gallery.js"

));
            bundles.Add(new ScriptBundle("~/bundles/Blueimp-Gallerry2").Include(//Blueimp Gallery 2 
"~/Scripts/blueimp-gallery2/js/blueimp-gallery.js",
"~/Scripts/blueimp-gallery2/js/blueimp-gallery-video.js",
"~/Scripts/blueimp-gallery2/js/blueimp-gallery-indicator.js",
"~/Scripts/blueimp-gallery2/js/jquery.blueimp-gallery.js"));
        }

    }
}
