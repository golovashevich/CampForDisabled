using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Web.Mvc;


namespace CustomValidation.Attributes
{
    /// <summary>
    /// Initial idea came from <see cref="http://nickstips.wordpress.com/2011/11/05/asp-net-mvc-lessthan-and-greaterthan-validation-attributes/"/>
    /// </summary>
    public class NumericLessThanAttribute : ValidationAttribute, IClientValidatable
    {
        private const string LESS_THAN_ERROR_MESSAGE = "{0} must be less than {1}.";
        private const string LESS_THAN_OR_EQUAL_TO_ERRORMESSAGE = "{0} must be less than or equal to {1}.";                

        public string OtherProperty { get; private set; }

        private string _otherPropertyTitle;
        private string OtherPropertyTitle
        { 
            get { return _otherPropertyTitle ?? (OtherProperty); } 
            set { _otherPropertyTitle = value; }
        }

        private bool allowEquality;

        public bool AllowEquality
        {
            get { return this.allowEquality; }
            set
            {
                this.allowEquality = value;
                
                // Set the error message based on whether or not
                // equality is allowed
                this.ErrorMessage = (value ? LESS_THAN_OR_EQUAL_TO_ERRORMESSAGE : LESS_THAN_ERROR_MESSAGE);
            }
        }        

        
        public NumericLessThanAttribute(string otherProperty)
            : base(LESS_THAN_ERROR_MESSAGE)
        {
            if (otherProperty == null) { throw new ArgumentNullException("otherProperty"); }

            this.OtherProperty = otherProperty;            
        }        


        public override string FormatErrorMessage(string name)
        {
            //http://connect.microsoft.com/VisualStudio/feedback/details/757298/emailaddress-attribute-is-unable-to-load-error-message-from-resource-mvc
            if (!String.IsNullOrEmpty(ErrorMessageResourceName))
            {
                ErrorMessage = null;
            }
            return String.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, OtherPropertyTitle);
        }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (null == value)
            {
                return null;
            }

            PropertyInfo otherPropertyInfo = validationContext.ObjectType.GetProperty(OtherProperty);   
            
            if (otherPropertyInfo == null)
            {
                return new ValidationResult(String.Format(CultureInfo.CurrentCulture, "Could not find a property named {0}.", OtherProperty));
            }

            object otherPropertyValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);

            if (null == otherPropertyValue)
            {
                return null;
            }

            decimal decValue;
            decimal decOtherPropertyValue;

            // Check to ensure the validating property is numeric
            if (!decimal.TryParse(value.ToString(), out decValue))
            {
                //TODO: Check that for globalization
                return new ValidationResult(String.Format(CultureInfo.CurrentCulture, "{0} is not a numeric value.", validationContext.DisplayName));
            }

            // Check to ensure the other property is numeric
            if (!decimal.TryParse(otherPropertyValue.ToString(), out decOtherPropertyValue))
            {
                return new ValidationResult(String.Format(CultureInfo.CurrentCulture, "{0} is not a numeric value.", OtherProperty));
            }

            // Check for equality
            if (AllowEquality && decValue == decOtherPropertyValue)
            {
                return null;
            }
            // Check to see if the value is greater than the other property value
            else if (decValue > decOtherPropertyValue)
            {
                TryToExtractOtherPropertyTitle(otherPropertyInfo);
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }            

            return null;
        }


        private void TryToExtractOtherPropertyTitle(PropertyInfo otherPropertyInfo)
        {
            if (null == otherPropertyInfo)
            {
                return;
            }
            var attributes = otherPropertyInfo.GetCustomAttributes(typeof(DisplayNameAttribute), false);
            if (null != attributes && attributes.Length > 0)
            {
                OtherPropertyTitle = ((DisplayNameAttribute)attributes[0]).DisplayName;
                return;
            }

            attributes = otherPropertyInfo.GetCustomAttributes(typeof(DisplayAttribute), false);
            if (null != attributes && attributes.Length > 0)
            {
                var attribute = (DisplayAttribute)attributes[0];
                if (null != attribute.ResourceType)
                {
                    ResourceManager manager = new ResourceManager(attribute.ResourceType);
                    OtherPropertyTitle = manager.GetString(attribute.Name);
                }
            }
        }


        public static string FormatPropertyForClientValidation(string property)
        {
            if (property == null)
            {
                throw new ArgumentException("Value cannot be null or empty.", "property");
            }
            return "*." + property;
        }       


        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, 
                ControllerContext context)
        {
            TryToExtractOtherPropertyTitle(metadata.ContainerType.GetProperty(OtherProperty));
            var rule = new ModelClientValidationNumericLessThanRule(FormatErrorMessage(metadata.DisplayName),
                    FormatPropertyForClientValidation(this.OtherProperty), this.AllowEquality);
            return new[] { rule };
        }
    }
}