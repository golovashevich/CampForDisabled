using System.Web.Mvc;

namespace Web.Controllers
{
    public class HomeController : CampControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }


		public ActionResult About()
        {
            return View();
        }
    }
}
