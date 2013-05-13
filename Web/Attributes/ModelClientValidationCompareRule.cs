using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Web.Attributes {
	public class ModelClientValidationCompareRule : ModelClientValidationRule {
		public ModelClientValidationCompareRule(string errorMessage, string otherProperty, 
				ValidationCompareOperator compareOperator)
        {
            ErrorMessage = errorMessage;
			//TODO: Check that this does not interfere with MVC's CompareAttribute
            ValidationType = "compare";
            ValidationParameters["otherProperty"] = otherProperty;
            ValidationParameters["compareOperator"] = compareOperator;
        }
	}
}