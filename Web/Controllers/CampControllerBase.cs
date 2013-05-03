using System;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class CampControllerBase : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
			ViewBag.CanUpdate = Request.IsAuthenticated;
			string cultureName = Convert.ToString(ControllerContext.RouteData.Values["language"]);
			if (null == cultureName)
			{
				cultureName = "en";
			}
			else if (!cultureName.StartsWith("ru"))
			{
				cultureName = "en";
			}
			
			var currentCulture = new CultureInfo(cultureName);

			Thread.CurrentThread.CurrentCulture = currentCulture;
			Thread.CurrentThread.CurrentUICulture = currentCulture;
		}
    }
}
