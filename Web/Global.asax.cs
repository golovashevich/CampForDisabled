using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Web.Controllers;

namespace Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.([iI][cC][oO]|[gG][iI][fF])(/.*)?" });

            routes.MapRoute(
                "Default", // Route name
                "{language}/{controller}/{action}/{id}", // URL with parameters
                new { language = "en", controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            RegisterCampControllerFactory();
        }


        private void RegisterCampControllerFactory()
        {
            IControllerFactory factory = new CampControllerFactory();
            ControllerBuilder.Current.SetControllerFactory(factory);
        }
    }
}