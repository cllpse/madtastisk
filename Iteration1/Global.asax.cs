using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace Iteration1
{
    public class MvcApplication : HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute
            (
                "Output format extension - query",
                "Iteration1/{controller}/{action}/{query}.{alternateOutputFormat}",
                new { alternateOutputFormat = "html" }
            );

            routes.MapRoute
            (
                "Output format extension - action",
                "Iteration1/{controller}/{action}.{alternateOutputFormat}",
                new { alternateOutputFormat = "html" }
            );
        }


        protected void Application_Start()
        {   
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
        }


        protected void Application_BeginRequest()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(1030);
        }
    }
}