using System.Web.Mvc;
using Camp.Models;
using Camp.Interfaces;

namespace Web.Controllers
{
    public class CampControllerBase : Controller
    {
        private readonly ICampDB _campDB;
        protected ICampDB CampDB
        {
            get { return _campDB; }
        }


        public CampControllerBase(ICampDB campDB)
        {
            _campDB = campDB;
        }


        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            ViewBag.CanUpdate = Request.IsAuthenticated;
        }
    }
}
