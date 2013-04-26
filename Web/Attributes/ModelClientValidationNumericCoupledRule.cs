using System.Web.Mvc;

namespace CustomValidation.Attributes
{
    public class ModelClientValidationNumericCoupledRule : ModelClientValidationRule
    {
        public ModelClientValidationNumericCoupledRule(string errorMessage, string other)
        {
            ErrorMessage = errorMessage;
            ValidationType = "numericcoupled";
            ValidationParameters["other"] = other;
        }
    }
}