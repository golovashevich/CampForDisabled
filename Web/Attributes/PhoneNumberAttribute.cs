using Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Validation.Attributes;

//Validation algorithm:
//Remove all _()-#
// Allowed symbols are "+" at the beginning and digits.
// If "+" is at the beginning, there must be 11 or 12 digits.
// Min length is 5 digits
// Max length is 20 digits


namespace CustomValidation.Attributes {
	public class PhoneNumberAttribute : BaseValidationAttribute, IClientValidatable {
		// Defaults:
		private const int MIN_DIGITS_COUNT = 5;
		private const int MAX_DIGITS_COUNT = 20;
		private const string ALLOWED_WASTE_SYMBOLS = @"\s|\(|\)|\-|\—|\#|_";


		//TODO: Different messages for each situation
		protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
			if (null == value) {
				return null;
			}

			string phone = (string)value;

			phone = Regex.Replace(phone, ALLOWED_WASTE_SYMBOLS, "");
			if (!Regex.IsMatch(phone, @"^( +)?\+?[- 0-9#()]+?$")) {
				ErrorMessage = ValidationMessages.PhoneInvalidSymbols;
				return new ValidationResult(ErrorMessage);
			}

			string onlyDigitsPhone = Regex.Replace(phone, @"[^\d]", String.Empty);

			if (phone[0] == '+') {
				if (onlyDigitsPhone.Length != 11 && onlyDigitsPhone.Length != 12) {
					ErrorMessage = ValidationMessages.PhoneStartedWithPlus;
					return new ValidationResult(ErrorMessage);
				}
			} else {
				if (onlyDigitsPhone.Length < MIN_DIGITS_COUNT || onlyDigitsPhone.Length > MAX_DIGITS_COUNT) {
					ErrorMessage = String.Format(CultureInfo.CurrentUICulture,
						ValidationMessages.PhoneDigitCountNotInTheRange, MIN_DIGITS_COUNT, MAX_DIGITS_COUNT);
					return new ValidationResult(ErrorMessage);
				}
			}
			return ValidationResult.Success;
		}


		public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context) {
			yield return new ModelClientPhoneNumberValidatorRule(ErrorMessage);
		}
	}
}