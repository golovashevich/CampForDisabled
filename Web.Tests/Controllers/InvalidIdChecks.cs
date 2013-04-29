using System.Web.Mvc;
using Camp.Interfaces;

namespace Web.Tests.Controllers
{
    internal class InvalidIdChecks<T>
    {
        private readonly ICampEntityController<T> _controller;
        private readonly string _controllerName;

        public InvalidIdChecks(ICampEntityController<T> controller)
        {               
            _controller = controller;
            _controllerName = controller.GetType().Name;
        }


        public void NullIdRedirectsToIndex()
        {
            Details_NullIdRedirectsToIndex();
            Delete_NullIdRedirectsToIndex();
            Edit_NullIdRedirectsToIndex();
        }


        public void Details_NullIdRedirectsToIndex()
        {
            ActionResult result = _controller.Details(null);
            ControllerUtils.CheckForRedirectToIndex(result, _controllerName, "Details");
        }


        public void Delete_NullIdRedirectsToIndex()
        {
            ActionResult result = _controller.Delete(null);
            ControllerUtils.CheckForRedirectToIndex(result, _controllerName, "Delete");
        }


        public void Edit_NullIdRedirectsToIndex()
        {
            ActionResult result = _controller.Edit(null);
            ControllerUtils.CheckForRedirectToIndex(result, _controllerName, "[Get]Edit");

            result = _controller.Edit(null);
            ControllerUtils.CheckForRedirectToIndex(result, _controllerName, "[Post]Edit");
        }
    }
}
