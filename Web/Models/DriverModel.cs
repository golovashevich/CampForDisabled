using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Resources;
using Validation.Attributes;


namespace Camp.Models
{
    [Table("Driver")]
    public class DriverModel
    {
        #region Public properties
        [HiddenInput]
        public virtual int Id { get; set; }

        [Display(Name = "ModelFullName", ResourceType = typeof(Drivers))]
        public virtual string FullName
        {
            get { return new FullNameComposer(FirstName, LastName);  }
        }


        public string DeleteConfirmationString
        {
            get { return String.Format(Drivers.DeleteConfirmation, FullName); }
        }

        
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationMessages))]
        [Display(Name = "ModelFirstName", ResourceType = typeof(Drivers))]
        [StringLength(30, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(ValidationMessages))]
        public virtual string FirstName { get; set; }


        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationMessages))]
        [Display(Name = "ModelLastName", ResourceType = typeof(Drivers))]
        [StringLength(30, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(ValidationMessages))]
        public virtual string LastName { get; set; }


        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationMessages))]
        [Display(Name = "ModelContacts", ResourceType = typeof(Drivers))]
        [DataType(DataType.MultilineText)]
        public virtual string Contacts { get; set; }


		[Display(Name = "ModelSitPlacesNum", ResourceType = typeof(Drivers))]
		[CompareOperator(ValidationDataType.Integer, ErrorMessageResourceName = "Integer", 
				ErrorMessageResourceType = typeof(ValidationMessages))]
		[Range(1, 19, ErrorMessageResourceName = "Range", ErrorMessageResourceType = typeof(ValidationMessages))]
		public virtual int? SitPlacesNum { get; set; }


		[Range(0, 19, ErrorMessageResourceName = "Range", ErrorMessageResourceType = typeof(ValidationMessages))]
        [Display(Name = "ModelWheelchairsNum", ResourceType = typeof(Drivers))]
		[CompareOperator(ValidationDataType.Integer, ErrorMessageResourceName = "Integer",
				ErrorMessageResourceType = typeof(ValidationMessages))]
		[CompareOperator("SitPlacesNum", ValidationCompareOperator.LessThanEqual, ValidationDataType.Integer,
				ErrorMessageResourceName = "NumericLessThanOrEqual", ErrorMessageResourceType = typeof(ValidationMessages))]
		public virtual int? WheelchairsNum { get; set; }


        [DataType(DataType.MultilineText)]
        [Display(Name = "ModelComments", ResourceType = typeof(Drivers))]
        public virtual string Comments { get; set; }
        #endregion

		public override bool Equals(object obj) {
			var other = obj as DriverModel; 
			if (other == null) {
				return false; 
			}
			return FirstName == other.FirstName || LastName == other.LastName || Contacts == other.Contacts
				|| SitPlacesNum == other.SitPlacesNum || WheelchairsNum == other.WheelchairsNum
				|| Comments == other.Comments;
		}


		public override int GetHashCode() {
			return base.GetHashCode();
		}
    }
}