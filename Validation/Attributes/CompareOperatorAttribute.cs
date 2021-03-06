﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Validation.Attributes {
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
	public class CompareOperatorAttribute : BaseTypeCheckAttribute, IClientValidatable {
		//TODO: Add default [globalized] messages

		public CompareOperator Operator { get; set; }

		private string _otherProperty;
		public string OtherProperty { 
			get { return _otherProperty; }
			set {
				if (value == null) {
					throw new ArgumentNullException("OtherProperty");
				}
				_otherProperty = value;
			} 
		}

		private string _otherPropertyTitle;
		protected string OtherPropertyTitle {
			get { return _otherPropertyTitle ?? (OtherProperty); }
			set { _otherPropertyTitle = value; }
		}

		public CompareOperatorAttribute(string otherProperty,
				CompareOperator compareOperator = CompareOperator.Equal,
				ValidationDataType dataType = ValidationDataType.String) {
			if (otherProperty == null) {
				throw new ArgumentNullException("otherProperty");
			}

			_otherProperty = otherProperty;
			Operator = compareOperator;
			Type = dataType;
		}

		private delegate bool CompareDelegate(IComparable one, IComparable two);
		private static Dictionary<CompareOperator, CompareDelegate> Comparers = CreateComparers();

		private static Dictionary<CompareOperator, CompareDelegate> CreateComparers() {
			var comparers = new Dictionary<CompareOperator, CompareDelegate>();
			comparers[CompareOperator.Equal] = (one, two) => one.CompareTo(two) == 0;
			comparers[CompareOperator.GreaterThan] = (one, two) => one.CompareTo(two) > 0;
			comparers[CompareOperator.GreaterThanEqual] = (one, two) => one.CompareTo(two) >= 0;
			comparers[CompareOperator.LessThan] = (one, two) => one.CompareTo(two) < 0;
			comparers[CompareOperator.LessThanEqual] = (one, two) => one.CompareTo(two) <= 0;
			comparers[CompareOperator.NotEqual] = (one, two) => one.CompareTo(two) != 0;
			return comparers;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
			PropertyInfo otherPropertyInfo = validationContext.ObjectType.GetProperty(OtherProperty);
			if (otherPropertyInfo == null) {
				//TODO: Globalization
				return new ValidationResult(String.Format(CultureInfo.CurrentCulture,
						"Could not find a property named {0}.", OtherProperty));
			}

			object otherValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);

			if (null == value && null == otherValue) {
				return null;
			}

			if (null == value || null == otherValue) {
				return null;
			}

			ValidationDataType valueType;
			if (!TypeDataTypeMap.TryGetValue(value.GetType(), out valueType)) {
				return null; //we can't judge this pair as know nothing about them
			}

			if (Type != ValidationDataType.String) {
				return CompareNonString(value, validationContext, otherPropertyInfo, otherValue, 
						valueType);
			} else { //Type is string
				value = Convert.ToString(value);
				otherValue = Convert.ToString(otherValue);
				if (String.IsNullOrWhiteSpace((string)value) 
						|| String.IsNullOrWhiteSpace((string)otherValue)) {
					return null;
				}
				if (!Comparers[Operator]((IComparable)value, (IComparable)otherValue)) {
					TryToExtractOtherPropertyTitle(otherPropertyInfo);
					return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
				}
			}
			return null;
		}

		private ValidationResult CompareNonString(object value, ValidationContext validationContext, 
			PropertyInfo otherPropertyInfo, object otherValue, ValidationDataType valueType) {
			if (valueType == ValidationDataType.String) {
				var otherValueString = Convert.ToString(otherValue);
				if (String.IsNullOrWhiteSpace((string)value) || String.IsNullOrWhiteSpace(otherValueString)) {
					return null;
				}
				try {
					value = Convert.ChangeType(value, DataTypeTypeMap[Type]);
					otherValue = Convert.ChangeType(otherValue, DataTypeTypeMap[Type]);
				}
				catch {
					return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
				}
			} else {
				ValidationDataType otherValueType;
				if (!TypeDataTypeMap.TryGetValue(otherValue.GetType(), out otherValueType)) {
					return null; //we can't judge this pair as know nothing about them
				}

				if (valueType != Type || otherValueType != Type) {
					try {
						value = Convert.ChangeType(value, DataTypeTypeMap[Type]);
						otherValue = Convert.ChangeType(otherValue, DataTypeTypeMap[Type]);
					}
					catch (InvalidCastException) {
						return null; //we can't judge this pair as know nothing about them
					}
				}

				if (!(value is IComparable) || !(otherValue is IComparable)) {
					TryToExtractOtherPropertyTitle(otherPropertyInfo);
					return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
				}
			}
			if (!Comparers[Operator]((IComparable)value, (IComparable)otherValue)) {
				TryToExtractOtherPropertyTitle(otherPropertyInfo);
				return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
			}
			return null;
		}

		/// <summary>
		/// <see cref="http://www.paraesthesia.com/archive/2010/03/02/the-importance-of-typeid-in-asp.net-mvc-dataannotations-validation-attributes.aspx"/>
		/// </summary>
		/// 
		private object _typeId;
		public override object TypeId {
			[DebuggerStepThrough]
			get {
				return _typeId ?? (_typeId = new object());
			}
		}

		protected void TryToExtractOtherPropertyTitle(PropertyInfo otherPropertyInfo) {
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

		public override string FormatErrorMessage(string name) {
			return String.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, OtherPropertyTitle);
		}

		public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata,
				ControllerContext context) {
			var displayName = metadata.DisplayName ?? metadata.PropertyName;
			TryToExtractOtherPropertyTitle(metadata.ContainerType.GetProperty(OtherProperty));
			var errorMessage = FormatErrorMessage(displayName);

			string otherProperty = FormatPropertyForClientValidation(OtherProperty);
			var rule = new ModelClientValidationCompareRule(errorMessage, otherProperty, Type.ToString(),
					Operator);
			return new[] { rule };
		}
	}
}