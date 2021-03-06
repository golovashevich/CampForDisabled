﻿using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Web.Attributes;
using Web.Controllers;

namespace Web
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			AuthConfig.RegisterAuth();
			RegisterCampControllerFactory();

			TurnOffStandardValueTypeValidators();

			ModelBinders.Binders.DefaultBinder = new CustomModelBinder();
		}


		//Turn off standard validators for model's ValueType fields
		private static void TurnOffStandardValueTypeValidators() {
			foreach (ModelValidatorProvider prov in ModelValidatorProviders.Providers) {
				if (prov.GetType().Equals(typeof(ClientDataTypeModelValidatorProvider))) {
					ModelValidatorProviders.Providers.Remove(prov);
					break;
				}
			}
		}


		private void RegisterCampControllerFactory()
		{
			IControllerFactory factory = new CampControllerFactory();
			ControllerBuilder.Current.SetControllerFactory(factory);
		}
	}
}