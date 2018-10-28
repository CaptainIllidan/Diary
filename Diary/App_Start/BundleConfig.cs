using System.Web;
using System.Web.Optimization;

namespace Diary
{
    public class BundleConfig
    {
        // Дополнительные сведения об объединении см. на странице https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.3.1.js",
                        "~/Scripts/jquery-3.3.1.min.js",
                        "~/Scripts/jquery-ui-1.12.1.js",
                        "~/Scripts/jquery-ui-timepicker-addon.js",
                        "~/Scripts/jquery-ui-timepicker-addon.min.js",
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.jqGrid.js",
                        "~/Scripts/jquery.jqGrid.min.js",
                        "~/Scripts/i18n/jquery-ui-timepicker-ru.js",
                        "~/Scripts/i18n/grid.locale-ru.js",
                        "~/Scripts/datepicker-ru.js",                        
                        "~/Scripts/moment.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // готово к выпуску, используйте средство сборки по адресу https://modernizr.com, чтобы выбрать только необходимые тесты.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/site.css",
                      "~/Content/bootstrap.css",
                      "~/Content/themes/base/jquery-ui.css",
                      "~/Content/jquery-ui-timepicker-addon.css",
                      "~/Content/jquery.jqGrid/ui.jqgrid.css"
                      ));
        }
    }
}
