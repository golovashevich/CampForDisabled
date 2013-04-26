using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Web.Mvc;


namespace CustomValidation.Attributes
{
    public class PhoneNumberWithPlusValidator : ValidationAttribute, IClientValidatable
    {
		private const string PHONE_INVALID_ERRORMESSAGE =
			"Длина телефонного номер должна быть 11 или 12 цифр, если телефонный номер начинается с '+'";


		public PhoneNumberWithPlusValidator() : base() { }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (null == value)
            {
                return null;
            }

			if (!Regex.IsMatch((string)value, @"^( +)?\+([^\+]+)?$"))
			{
				return null;
			}

			string onlyDigitsPhone = Regex.Replace((string)value, @"[^\d]", String.Empty);

			if (onlyDigitsPhone.Length == 11 || onlyDigitsPhone.Length == 12)
			{
				return null;
			}

            return new ValidationResult(PHONE_INVALID_ERRORMESSAGE);
        }


		public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
		{
			yield return new ModelClientPhoneNumberWithPlusValidatorRule(PHONE_INVALID_ERRORMESSAGE);
		}
    }
}