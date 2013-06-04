using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Camp.Interfaces;
using CustomValidation.Attributes;
using DataAnnotationsExtensions;
using Resources;


//TODO: Adjust string lengths accroding to SQL

namespace Camp.Models
{
	[Table("Camper")]
	public class CamperModel : ICampModel
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


		[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationMessages))]
		[Display(Name = "ModelFirstName", ResourceType = typeof(Campers))]
		[StringLength(30, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(ValidationMessages))]
		public virtual string FirstName { get; set; }


		[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationMessages))]
		[Display(Name = "ModelLastName", ResourceType = typeof(Campers))]
		[StringLength(30, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(ValidationMessages))]
		public virtual string LastName { get; set; }


		[Display(Name = "ModelDateOfBirth", ResourceType = typeof(Campers))]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
		[DataType(DataType.Date)]
		public virtual DateTime? DateOfBirth { get; set; }

		// TODO: Add foto
		// public virtual Image Foto { get; set; }


		[Display(Name = "ModelCityId", ResourceType = typeof(Campers))]
		[DefaultValue(1)]
		[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationMessages))]
		public virtual int? CityId { get; set; }


		[Display(Name = "ModelCity", ResourceType = typeof(Campers))]
		public virtual string City {
			get { 
				return CityId == 1 ? Campers.ModelKharkov : Campers.ModelAnotherCity; 
			}
			// TODO: Add Cities table in Database
		}


		[Display(Name = "ModelDistrict", ResourceType = typeof(Campers))]
		[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ValidationMessages))]
		[StringLength(30, ErrorMessageResourceName = "StringLength", 
				ErrorMessageResourceType = typeof(ValidationMessages))]
		public virtual string District { get; set; }


		[Display(Name = "ModelStreet", ResourceType = typeof(Campers))]
		[Required(ErrorMessageResourceName = "Required",
				ErrorMessageResourceType = typeof(ValidationMessages))]
		[StringLength(40, ErrorMessageResourceName = "StringLength", 
				ErrorMessageResourceType = typeof(ValidationMessages))]
		public virtual string Street { get; set; }


		[Display(Name = "ModelHomeNumber", ResourceType = typeof(Campers))]
		[Required(ErrorMessageResourceName = "Required",
				ErrorMessageResourceType = typeof(ValidationMessages))]
		[StringLength(10, ErrorMessageResourceName = "StringLength",
				ErrorMessageResourceType = typeof(ValidationMessages))]
		public virtual string HomeNumber { get; set; }


		[Display(Name = "ModelAppartmentNumber", ResourceType = typeof(Campers))]
		[StringLength(10, ErrorMessageResourceName = "StringLength", 
				ErrorMessageResourceType = typeof(ValidationMessages))]
		public virtual string AppartmentNumber { get; set; }


		[Display(Name = "ModelHomePhone", ResourceType = typeof(Campers))]
		[StringLength(20, ErrorMessageResourceName = "StringLength",
			ErrorMessageResourceType = typeof(ValidationMessages))]
		[PhoneNumber(ErrorMessageResourceName = "PhoneNumber", 
				ErrorMessageResourceType = typeof(ValidationMessages))]
		public virtual string HomePhone { get; set; }


		[Display(Name = "ModelAddressNote", ResourceType = typeof(Campers))]
		[DataType(DataType.MultilineText)]
		public virtual string AddressNote { get; set; }


		[Display(Name = "ModelContacts", ResourceType = typeof(Campers))]
		[Required(ErrorMessageResourceName = "ModelContactsRequired", 
				ErrorMessageResourceType = typeof(Campers))]
		[DataType(DataType.MultilineText)]
		public virtual string Contacts { get; set; }


		[Display(Name = "ModelEmail", ResourceType = typeof(Campers))]
		[StringLength(100, ErrorMessageResourceName = "StringLength", 
				ErrorMessageResourceType = typeof(ValidationMessages))]
		[Email(ErrorMessageResourceName = "Email", 
				ErrorMessageResourceType = typeof(ValidationMessages))]
        public virtual string Email { get; set; }


		[Display(Name = "ModelSkype", ResourceType = typeof(Campers))]
		[StringLength(40, ErrorMessageResourceName = "StringLength", 
				ErrorMessageResourceType = typeof(ValidationMessages))]
		public virtual string Skype { get; set; }


		[Display(Name = "ModelDisabilityGrade", ResourceType = typeof(Campers))]
		[DefaultValue(0)]
		public virtual int? DisabilityGrade { get; set; }

		public string DisabilityGradeString {
			get {
				if (DisabilityGrade == null) {
					return Campers.DisabilityGradeUnknown;
				}

				switch (DisabilityGrade) {
					case 0:
						return Campers.DisabilityGradeUnknown;
					case 1:
						return Campers.DisabilityGradeWalking;
					case 2:
						return Campers.DisabilityGradeHardWalking;
					case 3:
						return Campers.DisabilityGradeWheelchair;
					case 4:
						return Campers.DisabilityGradeBedPatient;
					
					default:
						return Campers.DisabilityGradeUnknown;
				}
			}
		}

		[Display(Name = "ModelMedicalNote", ResourceType = typeof(Campers))]
		[DataType(DataType.MultilineText)]
		public virtual string MedicalNote { get; set; }


		[Display(Name = "ModelComments", ResourceType = typeof(Campers))]
		[DataType(DataType.MultilineText)]
		public virtual string Comments { get; set; }
		#endregion
	}
}