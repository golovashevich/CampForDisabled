using System.Web.Mvc;

namespace CustomValidation.Attributes
{
    public class ModelClientPhoneNumberWithPlusValidatorRule : ModelClientValidationRule
    {
		public ModelClientPhoneNumberWithPlusValidatorRule(string errorMessage)
        {
            ErrorMessage = errorMessage;
			ValidationType = "phonenumberwithplusvalidator";
        }
    }
}