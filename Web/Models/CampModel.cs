using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Camp.Interfaces;
using Resources;
using Validation.Attributes;

namespace Camp.Models
{
	[Table("Camp")]
	public class CampModel : ICampModel
	{
		#region Public properties
		[HiddenInput]
		public virtual int Id { get; set; }

		public string DeleteConfirmationString {
			get { return String.Format(Camps.DeleteConfirmation, Name); }
		}

		public string CampName {
			get {
				if (BeginDate == null) {
					return null;
				}
				return String.Format("{0}, {1}", BeginDate.Value.Year, Name);
			}
		}

		[Display(Name = "ModelName", ResourceType = typeof(Camps))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationMessages))]
		[StringLength(50, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(ValidationMessages))]
		public virtual string Name { get; set; }

		[Display(Name = "ModelTheme", ResourceType = typeof(Camps))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationMessages))]
		[StringLength(50, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(ValidationMessages))]
		public virtual string Theme { get; set; }

		[Display(Name = "ModelBeginDate", ResourceType = typeof(Camps))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationMessages))]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
		[TypeCheck(ValidationDataType.Date, ErrorMessageResourceName = "Date",
				ErrorMessageResourceType = typeof(ValidationMessages))]
		public virtual DateTime? BeginDate { get; set; }

		[Display(Name = "ModelEndDate", ResourceType = typeof(Camps))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationMessages))]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
		[TypeCheck(ValidationDataType.Date, ErrorMessageResourceName = "Date",
				ErrorMessageResourceType = typeof(ValidationMessages))]
		[CompareOperator("BeginDate", CompareOperator.GreaterThanEqual, ValidationDataType.Date,
				ErrorMessageResourceName = "DateGreaterThanEqual", ErrorMessageResourceType = typeof(ValidationMessages))]
		public virtual DateTime? EndDate { get; set; }

		[Display(Name = "ModelDescription", ResourceType = typeof(Camps))]
		[DataType(DataType.MultilineText)]
		public virtual string Description { get; set; }

		[Display(Name = "ModelHistory", ResourceType = typeof(Camps))]
		[DataType(DataType.MultilineText)]
		public virtual string History { get; set; }
		#endregion

		public override bool Equals(object obj) {
			CampModel other = obj as CampModel;
			if (other == null) {
				return false;
			}

			bool equal = Id == other.Id && Name == other.Name && Theme == other.Theme
					&& BeginDate == other.BeginDate && EndDate == other.EndDate
					&& Description == other.Description && History == other.History;

			return equal;
		}

		public override int GetHashCode() {
			return base.GetHashCode();
		}

		public override string ToString() {
			return CampName;
		}
	}
}