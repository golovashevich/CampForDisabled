using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web
{
	public class RouteConfig
	{
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
	}
}