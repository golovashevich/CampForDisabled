using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using Camp.Interfaces;
using Camp.Models;

namespace Web.Controllers
{
    public class CampControllerFactory : IControllerFactory
    {
        public IController CreateController(RequestContext requestContext, string controllerName)
        {
            string controllerTypeName = String.Format("{0}.{1}Controller", this.GetType().Namespace, controllerName);
            Type controllerType = Type.GetType(controllerTypeName);
			if (controllerType == null) {
				return null;
			}
            IController controller;
            var constructor = controllerType.GetConstructor(new Type[] { typeof(ICampDB) });

            if (null != constructor)
            {
                controller = constructor.Invoke(new [] { new CampDB(true) }) as IController;
            }
            else
            {
                controller = Activator.CreateInstance(controllerType) as IController;
            }
            return controller;
        }


        public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
        {
            return SessionStateBehavior.Default;
        }


        public void ReleaseController(IController controller)
        {
            IDisposable disposable = controller as IDisposable;
            if (null != disposable)
            {
                disposable.Dispose();
            }
        }
    }
}