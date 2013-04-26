using System.Web.Mvc;

namespace CustomValidation.Attributes
{
    public class ModelClientPhoneDigitsCountRangeRule : ModelClientValidationRule
    {
        public ModelClientPhoneDigitsCountRangeRule(string errorMessage, int max, int min)
        {
            ErrorMessage = errorMessage;
			ValidationParameters["max"] = max;
			ValidationParameters["min"] = min;
            ValidationType = "phonedigitscountrange";
        }
    }
}