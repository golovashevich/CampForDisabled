using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Web.Mvc;


namespace CustomValidation.Attributes
{
    public class PhoneDigitsCountRange : ValidationAttribute, IClientValidatable
    {
		// Defaults:
		private const int MIN_DIGITS_COUNT = 5;
		private const int MAX_DIGITS_COUNT = 20;

		private int _minDigitsCount;
		private int _maxDigitsCount;

		private const string PHONE_DIGITS_COUNT_IS_NOT_IN_RANGE_ERRORMESSAGE =
			"Количество цифр в телефонном номере должно быть от {0} до {1}";


		public PhoneDigitsCountRange() : this(MIN_DIGITS_COUNT, MAX_DIGITS_COUNT) { }


		public PhoneDigitsCountRange(int minDigitsCount, int maxDigitsCount) : base() 
		{
			_minDigitsCount = minDigitsCount;
			_maxDigitsCount = maxDigitsCount;

			// initializing error message:
			ErrorMessage = String.Format(PHONE_DIGITS_COUNT_IS_NOT_IN_RANGE_ERRORMESSAGE, 
				_minDigitsCount, _maxDigitsCount);
		}


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (null == value)
            {
                return null;
            }

			string onlyDigitsPhone = Regex.Replace((string)value, @"[^\d]", String.Empty);

			if (onlyDigitsPhone.Length < _minDigitsCount || onlyDigitsPhone.Length > _maxDigitsCount)
			{
				return new ValidationResult(ErrorMessage);
			}

            return null;
        }


		public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
		{
			yield return new ModelClientPhoneDigitsCountRangeRule(ErrorMessage, _maxDigitsCount, _minDigitsCount);
		}
    }
}