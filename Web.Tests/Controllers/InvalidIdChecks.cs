using System.Web.Mvc;
using Camp.Interfaces;

namespace Web.Tests.Controllers
{
    internal class InvalidIdChecks<T> where T: new()
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
            Details_NullIdRedirectsToIndex(null);
            Delete_NullIdRedirectsToIndex(null);
            Edit_NullIdRedirectsToIndex(null);
        }


		public void NonExistingIdRedirectsToIndex(int id) 
		{
			Details_NullIdRedirectsToIndex(id);
			Delete_NullIdRedirectsToIndex(id);
			Edit_NullIdRedirectsToIndex(id);
		}


		public void Details_NullIdRedirectsToIndex(int? invalidValue)
        {
			ActionResult result = _controller.Details(invalidValue);
            ControllerUtils.CheckForRedirectToIndex(result, _controllerName, "Details");
        }


		public void Delete_NullIdRedirectsToIndex(int? invalidValue)
        {
			ActionResult result = _controller.Delete(invalidValue);
            ControllerUtils.CheckForRedirectToIndex(result, _controllerName, "Delete");
        }


		public void Edit_NullIdRedirectsToIndex(int? invalidValue)
        {
			ActionResult result = _controller.Edit(invalidValue);
            ControllerUtils.CheckForRedirectToIndex(result, _controllerName, "[Get]Edit");

			result = _controller.Edit(invalidValue, new T());
            ControllerUtils.CheckForRedirectToIndex(result, _controllerName, "[Post]Edit");
        }
    }
}
