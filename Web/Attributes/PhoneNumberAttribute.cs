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
		private const string ALLOWED_SYMBOLS = @"^( +)?\+?[- 0-9#()]+?$";


		//TODO: Different messages for each situation
		protected override ValidationResult IsValid(object value, ValidationContext context) {
			if (null == value) {
				return null;
			}

			string phone = (string)value;

			if (string.IsNullOrWhiteSpace(phone)) {
				return null;
			}

			phone = Regex.Replace(phone, ALLOWED_WASTE_SYMBOLS, "");
			if (!Regex.IsMatch(phone, ALLOWED_SYMBOLS)) {
				return new ValidationResult(FormatErrorMessage(context.DisplayName));
			}

			string onlyDigitsPhone = Regex.Replace(phone, @"[^\d]", String.Empty);

			if (phone[0] == '+') {
				if (onlyDigitsPhone.Length != 11 && onlyDigitsPhone.Length != 12) {
				return new ValidationResult(FormatErrorMessage(context.DisplayName));
				}
			} else {
				if (onlyDigitsPhone.Length < MIN_DIGITS_COUNT || onlyDigitsPhone.Length > MAX_DIGITS_COUNT) {
					return new ValidationResult(FormatErrorMessage(context.DisplayName));
				}
			}
			return ValidationResult.Success;
		}


		public override string FormatErrorMessage(string name) {
			return String.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, 
					MIN_DIGITS_COUNT, MAX_DIGITS_COUNT);
		}


		public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, 
				ControllerContext context) {
			var displayName = metadata.DisplayName ?? metadata.PropertyName;
			var errorMessage = FormatErrorMessage(displayName);

			yield return new ModelClientPhoneNumberValidatorRule(errorMessage);
		}
	}
}