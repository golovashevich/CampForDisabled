using Resources;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;


namespace Camp.Models
{
	[Table("Camp")]
	public class CampModel
	{
		#region Public properties

        [HiddenInput]
		public virtual int Id { get; set; }

		[Display(Name = "ModelYear", ResourceType = typeof(Camps))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Validation))]
		[DefaultValue(2003)]
		public virtual int Year { get; set; }

		[Display(Name = "ModelName", ResourceType = typeof(Camps))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Validation))]
		[StringLength(50, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(Validation))]
		public virtual string Name { get; set; }

		public virtual string CampName {
			get {
				return String.Format("{0}, {1}", Year, Name);
			}
		}

		[Display(Name = "ModelTheme", ResourceType = typeof(Camps))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Validation))]
		[StringLength(50, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(Validation))]
		public virtual string Theme { get; set; }

		[Display(Name = "ModelBeginDate", ResourceType = typeof(Camps))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Validation))]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
		[DataType(DataType.Date)]
		public virtual DateTime? BeginDate { get; set; }

		[Display(Name = "ModelEndDate", ResourceType = typeof(Camps))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Validation))]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
		[DataType(DataType.Date)]
		public virtual DateTime? EndDate { get; set; }

		[Display(Name = "ModelDescription", ResourceType = typeof(Camps))]
		[DataType(DataType.MultilineText)]
		public virtual string Description { get; set; }

		[Display(Name = "ModelHistory", ResourceType = typeof(Camps))]
		[DataType(DataType.MultilineText)]
		public virtual string History { get; set; }

		public string DeleteConfirmationString
		{
			get { return String.Format(Camps.DeleteConfirmation, Name); }
		}
		#endregion
	}
}