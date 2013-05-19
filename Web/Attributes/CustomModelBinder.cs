using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;

namespace Web.Attributes {
	/// <summary>
	/// Enables exponential for for integer field values
	/// </summary>
	public class CustomModelBinder : DefaultModelBinder {
		protected override void SetProperty(ControllerContext controllerContext,
				ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor,
				object value) {
			if (propertyDescriptor.PropertyType == typeof(int?)) {
				var originaValue = controllerContext.HttpContext.Request[propertyDescriptor.Name];
				int newValue;
				if (Int32.TryParse(originaValue, NumberStyles.AllowExponent,
						Thread.CurrentThread.CurrentCulture, out newValue)) {
					bindingContext.ModelState[propertyDescriptor.Name].Errors.Clear();
					propertyDescriptor.SetValue(bindingContext.Model, newValue);
					return;
				}
			}
			base.SetProperty(controllerContext, bindingContext, propertyDescriptor, value);
		}
	}
}