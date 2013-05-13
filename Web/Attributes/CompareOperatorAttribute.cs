﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Web.Attributes {
	public class CompareOperatorAttribute : ValidationAttribute, IClientValidatable{
		public string OtherProperty { get; private set; }

		public ValidationCompareOperator Operator { get; set; }
		public ValidationDataType Type { get; set; }


		private string _otherPropertyTitle;
		private string OtherPropertyTitle {
			get { return _otherPropertyTitle ?? (OtherProperty); }
			set { _otherPropertyTitle = value; }
		}


		public CompareOperatorAttribute(ValidationDataType dataType) : 
				this(null, ValidationCompareOperator.DataTypeCheck, dataType) {
		}


		public CompareOperatorAttribute(string otherProperty,
				ValidationCompareOperator compareOperator = ValidationCompareOperator.Equal,
				ValidationDataType dataType = ValidationDataType.String) {
			if (compareOperator != ValidationCompareOperator.DataTypeCheck) {
				if (otherProperty == null) {
					throw new ArgumentNullException("otherProperty");
				}
			}

			OtherProperty = otherProperty;
			Operator = compareOperator;
			Type = dataType;
		}


		private static Dictionary<Type, ValidationDataType> TypeDataTypeMap =  
				new Dictionary<Type, ValidationDataType>() {
						{ typeof(DateTime), ValidationDataType.Date },
						{ typeof(String), ValidationDataType.String },
						{ typeof(Int32), ValidationDataType.Integer },
						{ typeof(Double), ValidationDataType.Double },
						{ typeof(Decimal), ValidationDataType.Currency }
					};

		private static Dictionary<ValidationDataType, Type> DataTypeTypeMap =
				new Dictionary<ValidationDataType, Type>() {
						{ ValidationDataType.Date, typeof(DateTime) },
						{ ValidationDataType.String, typeof(String) },
						{ ValidationDataType.Integer, typeof(Int32) },
						{ ValidationDataType.Double, typeof(Double) },
						{ ValidationDataType.Currency, typeof(Decimal) }
					};


		private delegate bool CompareDelegate(IComparable one, IComparable two);
		private static Dictionary<ValidationCompareOperator, CompareDelegate> Comparers = CreateComparers();


		private static Dictionary<ValidationCompareOperator, CompareDelegate> CreateComparers() {
			var comparers = new Dictionary<ValidationCompareOperator, CompareDelegate>();
			comparers[ValidationCompareOperator.Equal] = (one, two) => one.CompareTo(two) == 0;
			comparers[ValidationCompareOperator.GreaterThan] = (one, two) => one.CompareTo(two) > 0;
			comparers[ValidationCompareOperator.GreaterThanEqual] = (one, two) => one.CompareTo(two) >= 0;
			comparers[ValidationCompareOperator.LessThan] = (one, two) => one.CompareTo(two) < 0;
			comparers[ValidationCompareOperator.LessThanEqual] = (one, two) => one.CompareTo(two) <= 0;
			comparers[ValidationCompareOperator.NotEqual] = (one, two) => one.CompareTo(two) != 0;
			return comparers;
		}


		//TODO: Divide into smaller methods
		protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
			if (Operator == ValidationCompareOperator.DataTypeCheck) {
				return IsValidDataType(value, validationContext);
			}
			PropertyInfo otherPropertyInfo = validationContext.ObjectType.GetProperty(OtherProperty);

			if (otherPropertyInfo == null) {
				//TODO: Globalization
				return new ValidationResult(String.Format(CultureInfo.CurrentCulture, 
						"Could not find a property named {0}.", OtherProperty));
			}

			object otherPropertyValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);

			if (null == value && null == otherPropertyValue) {
				return null;
			}

			if (null == value || null == otherPropertyValue) {
				TryToExtractOtherPropertyTitle(otherPropertyInfo);
				return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
			}

			ValidationDataType valueType;
			if (!TypeDataTypeMap.TryGetValue(value.GetType(), out valueType)) {
				TryToExtractOtherPropertyTitle(otherPropertyInfo);
				return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
			}

			if (Type != ValidationDataType.String) {
				if (valueType == ValidationDataType.String) {
					var otherValueString = Convert.ToString(otherPropertyValue);
					if (String.IsNullOrEmpty((string)value) || String.IsNullOrEmpty(otherValueString)) {
						return null;
					}
					try {
						value = Convert.ChangeType(value, DataTypeTypeMap[Type]);
						otherPropertyValue = Convert.ChangeType(otherPropertyValue, DataTypeTypeMap[Type]);
					}
					catch {
						return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
					}
				} else {

					if (value.GetType() != otherPropertyValue.GetType()) {
						TryToExtractOtherPropertyTitle(otherPropertyInfo);
						return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
					}

					if (valueType != Type) {
						TryToExtractOtherPropertyTitle(otherPropertyInfo);
						return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
					}

					if (!(value is IComparable) || !(otherPropertyValue is IComparable)) {
						TryToExtractOtherPropertyTitle(otherPropertyInfo);
						return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
					}
				}
				if (!Comparers[Operator]((IComparable)value, (IComparable)otherPropertyValue)) {
					TryToExtractOtherPropertyTitle(otherPropertyInfo);
					return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
				}
			}
			else { //Type is string
				if (!(value is String) || !(otherPropertyValue is String) 
					|| !Comparers[Operator]((IComparable)value, (IComparable)otherPropertyValue)) {
					TryToExtractOtherPropertyTitle(otherPropertyInfo);
					return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
				}
			}
			return null;
		}


		private ValidationResult IsValidDataType(object value, ValidationContext context) {
			if (null == value) {
 				return null;
			}

			ValidationDataType valueType;
			if (!TypeDataTypeMap.TryGetValue(value.GetType(), out valueType)) {
				return new ValidationResult(FormatErrorMessage(context.DisplayName));
			}

			if (valueType == Type) {
				return null;
			}

			if (Type == ValidationDataType.String) {
				return new ValidationResult(FormatErrorMessage(context.DisplayName));
			}

			if (valueType != ValidationDataType.String) {
				return new ValidationResult(FormatErrorMessage(context.DisplayName));
			}

			if (String.IsNullOrEmpty((string)value)) {
				return null;
			}
			try {
				Convert.ChangeType(value, DataTypeTypeMap[Type]);
				return null;
			} 
			catch {
				return new ValidationResult(FormatErrorMessage(context.DisplayName));
			}
		}


		private void TryToExtractOtherPropertyTitle(PropertyInfo otherPropertyInfo) {
			if (null == otherPropertyInfo) {
				return;
			}
			var attributes = otherPropertyInfo.GetCustomAttributes(typeof(DisplayNameAttribute), false);
			if (null != attributes && attributes.Length > 0) {
				OtherPropertyTitle = ((DisplayNameAttribute)attributes[0]).DisplayName;
				return;
			}

			attributes = otherPropertyInfo.GetCustomAttributes(typeof(DisplayAttribute), false);
			if (null != attributes && attributes.Length > 0) {
				var attribute = (DisplayAttribute)attributes[0];
				if (null != attribute.ResourceType) {
					ResourceManager manager = new ResourceManager(attribute.ResourceType);
					OtherPropertyTitle = manager.GetString(attribute.Name);
				}
			}
		}


		public static string FormatPropertyForClientValidation(string property) {
			if (property == null) {
				throw new ArgumentException("Value cannot be null or empty.", "property");
			}
			return "*." + property;
		}       


		public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata,
				ControllerContext context) {
			TryToExtractOtherPropertyTitle(metadata.ContainerType.GetProperty(OtherProperty));
			var rule = new ModelClientValidationCompareRule(FormatErrorMessage(metadata.DisplayName),
					FormatPropertyForClientValidation(this.OtherProperty), Operator);
			return new[] { rule };
		}
	}
}