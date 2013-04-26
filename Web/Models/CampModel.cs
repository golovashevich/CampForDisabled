using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Camp.Models
{
	[Table("Camp")]
	public class CampModel
	{
		#region Public properties

		[Required]
		public virtual int Id { get; set; }

		[DisplayName("Год")]
		[Required(ErrorMessage = "Введите год проведения лагеря")]
		[DefaultValue(2003)]
		[Range(2003, 2103)] 
		public virtual int Year { get; set; }

		[DisplayName("Название")]
		[Required(ErrorMessage = "Введите название лагеря")]
		[StringLength(50)]
		public virtual string Name { get; set; }

		[DisplayName("Тема")]
        [Required(ErrorMessage = "Введите тему лагеря")]
		[StringLength(50)]
		public virtual string Theme { get; set; }

		[DisplayName("Начало")]
		[Required(ErrorMessage = "Введите дату начала лагеря")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
		[DataType(DataType.Date)]
		public virtual DateTime? BeginDate { get; set; }

		[DisplayName("Конец")]
		[Required(ErrorMessage = "Введите дату конца лагеря")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
		[DataType(DataType.Date)]
		public virtual DateTime? EndDate { get; set; }

		[DisplayName("Описание")]
		[DataType(DataType.MultilineText)]
		public virtual string Description { get; set; }

		[DisplayName("События")]
		[DataType(DataType.MultilineText)]
		public virtual string History { get; set; }

		#endregion
	}
}