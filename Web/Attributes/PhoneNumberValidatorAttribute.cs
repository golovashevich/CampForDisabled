using Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace CustomValidation.Attributes
{
    public class PhoneNumberValidator : ValidationAttribute, IClientValidatable
    {
		// Defaults:
		private const int MIN_DIGITS_COUNT = 5;
		private const int MAX_DIGITS_COUNT = 20;

		public PhoneNumberValidator() : base() { }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (null == value) {
                return null;
            }
			
			string phone = (string) value;
			if (!Regex.IsMatch(phone, @"^( +)?\+?[- 0-9#()]+?$")) {
				ErrorMessage = Validation.PhoneInvalidSymbols;
				return new ValidationResult(ErrorMessage);
			}

			phone = phone.Trim();
			string onlyDigitsPhone = Regex.Replace(phone, @"[^\d]", String.Empty);

			if (phone.ToCharArray()[0] == '+') {
				if (onlyDigitsPhone.Length != 11 && onlyDigitsPhone.Length != 12) {
					ErrorMessage = Validation.PhoneStartedWithPlus;
					return new ValidationResult(ErrorMessage);
				}
			}
			else {
				if (onlyDigitsPhone.Length < MIN_DIGITS_COUNT || onlyDigitsPhone.Length > MAX_DIGITS_COUNT) {
					ErrorMessage = String.Format(CultureInfo.CurrentUICulture,
						Validation.PhoneDigitCountNotInTheRange, MIN_DIGITS_COUNT, MAX_DIGITS_COUNT);
					return new ValidationResult(ErrorMessage);
				}
			}
			return ValidationResult.Success;
        }


		public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
		{
			yield return new ModelClientPhoneNumberValidatorRule(ErrorMessage);
		}
    }
}