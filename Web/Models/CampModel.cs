using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Resources;


namespace Camp.Models
{
	[Table("Camp")]
	public class CampModel
	{
		#region Public properties

		[Required]
		public virtual int Id { get; set; }

		[Display(Name = "ModelYear", ResourceType = typeof(Camps))]
		[Required(ErrorMessageResourceName = "RequiredYear", ErrorMessageResourceType = typeof(Camps))]
		[DefaultValue(2003)]
		[Range(2003, 2103)] 
		public virtual int Year { get; set; }

		[Display(Name = "ModelName", ResourceType = typeof(Camps))]
		[Required(ErrorMessageResourceName = "RequiredName", ErrorMessageResourceType = typeof(Camps))]
		[StringLength(50)]
		public virtual string Name { get; set; }

		[Display(Name = "ModelTheme", ResourceType = typeof(Camps))]
		[Required(ErrorMessageResourceName = "RequiredTheme", ErrorMessageResourceType = typeof(Camps))]
		[StringLength(50)]
		public virtual string Theme { get; set; }

		[Display(Name = "ModelBeginDate", ResourceType = typeof(Camps))]
		[Required(ErrorMessageResourceName = "RequiredBeginDate", ErrorMessageResourceType = typeof(Camps))]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
		[DataType(DataType.Date)]
		public virtual DateTime? BeginDate { get; set; }

		[Display(Name = "ModelEndDate", ResourceType = typeof(Camps))]
		[Required(ErrorMessageResourceName = "RequiredEndDate", ErrorMessageResourceType = typeof(Camps))]
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