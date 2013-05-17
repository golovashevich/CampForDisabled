using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Web.Attributes {
	public class ModelClientValidationCompareRule : ModelClientValidationRule {
		//TODO: Handle situation when other property does not exists (data type check)
		public ModelClientValidationCompareRule(string errorMessage, string otherProperty, string dataType, 
				ValidationCompareOperator compareOperator)
        {
            ErrorMessage = errorMessage;
			//TODO: Check that this does not interfere with MVC's CompareAttribute
            ValidationType = "compareoperator";
            ValidationParameters["otherproperty"] = otherProperty;
			ValidationParameters["datatype"] = dataType;
            ValidationParameters["compareoperator"] = compareOperator;
        }
	}
}