using CustomValidation.Attributes;
using DataAnnotationsExtensions;
using Resources;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;


//TODO: Adjust string lengths accroding to SQL

namespace Camp.Models
{
	[Table("Camper")]
	public class CamperModel
	{
		#region Public properties

		[Required]
		[HiddenInput]
		public virtual int Id { get; set; }


		[Display(Name = "ModelFullName", ResourceType = typeof(Campers))]
		public virtual string FullName 
		{
            get { return new FullNameComposer(FirstName, LastName); }
        }


		public string DeleteConfirmationString
		{
			get { return String.Format(Campers.DeleteConfirmation, FullName); }
		}


		[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Validation))]
		[Display(Name = "ModelFirstName", ResourceType = typeof(Campers))]
		[StringLength(30, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(Validation))]
		public virtual string FirstName { get; set; }


		[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Validation))]
		[Display(Name = "ModelLastName", ResourceType = typeof(Campers))]
		[StringLength(30, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(Validation))]
		public virtual string LastName { get; set; }


		[Display(Name = "ModelDateOfBirth", ResourceType = typeof(Campers))]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
		[DataType(DataType.Date)]
		public virtual DateTime? DateOfBirth { get; set; }

		// TODO: Add foto
		// public virtual Image Foto { get; set; }

		//TODO: Globalize this
		[Range(0, 99999, ErrorMessageResourceName = "PostInfex", 
				ErrorMessageResourceType = typeof(Validation))]
		[Display(Name = "ModelPostIndex", ResourceType = typeof(Campers))]
		[Integer(ErrorMessageResourceName = "Integer", ErrorMessageResourceType = typeof(Validation))]
		[DisplayFormat(DataFormatString = "{0,5:D5}", ApplyFormatInEditMode = true)]
		public virtual int? PostIndex { get; set; }


		[Display(Name = "ModelCityId", ResourceType = typeof(Campers))]
		[DefaultValue(1)]
		[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Validation))]
		public virtual int? CityId { get; set; }


		[Display(Name = "ModelCity", ResourceType = typeof(Campers))]
		public virtual string City
		{
			get { return CityId == 1 ? "Харьков" : "Другой город"; }
			// TODO: Add Cities table in Database
		}


		[Display(Name = "ModelDistrict", ResourceType = typeof(Campers))]
		[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Validation))]
		[StringLength(30, ErrorMessageResourceName = "StringLength", 
				ErrorMessageResourceType = typeof(Validation))]
		public virtual string District { get; set; }


		[Display(Name = "ModelStreet", ResourceType = typeof(Campers))]
		[Required(ErrorMessageResourceName = "Required",
				ErrorMessageResourceType = typeof(Validation))]
		[StringLength(40, ErrorMessageResourceName = "StringLength", 
				ErrorMessageResourceType = typeof(Validation))]
		public virtual string Street { get; set; }


		[Display(Name = "ModelHomeNumber", ResourceType = typeof(Campers))]
		[Required(ErrorMessageResourceName = "Required",
				ErrorMessageResourceType = typeof(Validation))]
		[StringLength(10, ErrorMessageResourceName = "StringLength",
				ErrorMessageResourceType = typeof(Validation))]
		public virtual string HomeNumber { get; set; }


		[Display(Name = "ModelAppartmentNumber", ResourceType = typeof(Campers))]
		[StringLength(10, ErrorMessageResourceName = "StringLength", 
				ErrorMessageResourceType = typeof(Validation))]
		public virtual string AppartmentNumber { get; set; }


		//TODO: Get rid of waste validators
		//TODO: Get rid of Validator suffix
		//TODO: Globalize/localize PhoneNumberWithPlus
		[Display(Name = "ModelHomePhone", ResourceType = typeof(Campers))]
		[StringLength(20, ErrorMessageResourceName = "StringLength",
				ErrorMessageResourceType = typeof(Validation))]
		[DataType(DataType.PhoneNumber)]
		[RegularExpression(@"^( +)?\+?[- 0-9#()]+?$", ErrorMessageResourceName = "Phone",
				ErrorMessageResourceType = typeof(Validation))]
        [PhoneDigitsCountRange]
		[PhoneNumberWithPlusValidator]
		public virtual string HomePhone { get; set; }


		[Display(Name = "ModelAddressNote", ResourceType = typeof(Campers))]
		[DataType(DataType.MultilineText)]
		public virtual string AddressNote { get; set; }


		[Display(Name = "ModelContacts", ResourceType = typeof(Campers))]
		[Required(ErrorMessageResourceName = "ModelCommentsRequired", 
				ErrorMessageResourceType = typeof(Campers))]
		[DataType(DataType.MultilineText)]
		public virtual string Contacts { get; set; }


		[Display(Name = "ModelEmail", ResourceType = typeof(Campers))]
		[StringLength(100, ErrorMessageResourceName = "StringLength", 
				ErrorMessageResourceType = typeof(Validation))]
		[Email(ErrorMessageResourceName = "Email", 
				ErrorMessageResourceType = typeof(Validation))]
        public virtual string Email { get; set; }


		[Display(Name = "ModelSkype", ResourceType = typeof(Campers))]
		[StringLength(40, ErrorMessageResourceName = "StringLength", 
				ErrorMessageResourceType = typeof(Validation))]
		public virtual string Skype { get; set; }


		//TODO: Globalize DisabilityGradeEnum
		private enum DisabilityGradeEnum { Unknown, Walking, HardWalking, Wheelchair, BedPatient };
		//TODO: Make DisabilityGrade drop down list
		[Display(Name = "ModelDisabilityGrade", ResourceType = typeof(Campers))]
		[Range(0, 4, ErrorMessageResourceName = "Range",
				ErrorMessageResourceType = typeof(Validation))]
		[DefaultValue(0)]
		[EnumDataType(typeof(DisabilityGradeEnum), ErrorMessageResourceName = "EnumDataType", 
				ErrorMessageResourceType = typeof(Validation))] //temporary not working solution; to
				// be removed afte drop down list implementation 
		public virtual int? DisabilityGrade { get; set; }
		//TODO: Add DisabilityGrade enum or table 
		// 0 - Неизвестно
		// 1 - Ходящий
		// 2 - Тяжело ходящий
		// 3 - Коляска
		// 4 - Лежачий


		[Display(Name = "ModelMedicalNote", ResourceType = typeof(Campers))]
		[DataType(DataType.MultilineText)]
		public virtual string MedicalNote { get; set; }


		[Display(Name = "ModelComments", ResourceType = typeof(Campers))]
		[DataType(DataType.MultilineText)]
		public virtual string Comments { get; set; }
		#endregion
	}
}