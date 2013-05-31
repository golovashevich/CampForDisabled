using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Web.Tests.Controllers
{
    internal class ControllerUtils
    {
        internal static void CheckForRedirectToIndex(ActionResult result, string controllerName, string actionName)
        {
            string place = String.Format("/{0}/{1}: ", controllerName, actionName);
            Assert.IsNotNull(result, place + "Action result should not be null");
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult), place + "Invalid result type");
            var redirectResult = result as RedirectToRouteResult;
            Assert.AreEqual("Index", redirectResult.RouteValues["action"], place + " should redirect to Index on invalid id");
        }


		internal static void CheckForView(ActionResult result, string controllerName, string actionName) {
			string place = String.Format("/{0}/{1}: ", controllerName, actionName);
			Assert.IsNotNull(result, place + "Action result should not be null");
			Assert.IsInstanceOfType(result, typeof(ViewResult), place + "Invalid result type");
		}
	}
}
