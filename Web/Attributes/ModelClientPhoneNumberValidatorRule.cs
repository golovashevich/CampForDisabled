using System.Web.Mvc;

namespace CustomValidation.Attributes
{
    public class ModelClientPhoneNumberValidatorRule : ModelClientValidationRule
    {
		public ModelClientPhoneNumberValidatorRule(string errorMessage)
        {
            ErrorMessage = errorMessage;
			ValidationType = "phonenumber";
        }
    }
}