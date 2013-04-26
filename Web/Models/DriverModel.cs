using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using CustomValidation.Attributes;
using DataAnnotationsExtensions;
using Resources;


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

        
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Validation))]
        [Display(Name = "ModelFirstName", ResourceType = typeof(Drivers))]
        [StringLength(30, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(Validation))]
        public virtual string FirstName { get; set; }


        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Validation))]
        [Display(Name = "ModelLastName", ResourceType = typeof(Drivers))]
        [StringLength(30, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(Validation))]
        public virtual string LastName { get; set; }


        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Validation))]
        [Display(Name = "ModelContacts", ResourceType = typeof(Drivers))]
        [DataType(DataType.MultilineText)]
        public virtual string Contacts { get; set; }


        [Range(1, 19, ErrorMessageResourceName = "Range", ErrorMessageResourceType = typeof(Validation))]
        [Display(Name = "ModelSitPlacesNum", ResourceType = typeof(Drivers))]
        [NumericCoupled("WheelchairsNum")]
        [Integer(ErrorMessageResourceName = "Integer", ErrorMessageResourceType = typeof(Validation))]
        public virtual int? SitPlacesNum { get; set; }


        [Range(0, 19, ErrorMessageResourceName = "Range", ErrorMessageResourceType = typeof(Validation))]
        [Display(Name = "ModelWheelchairsNum", ResourceType = typeof(Drivers))]
        [NumericLessThan("SitPlacesNum", AllowEquality = true, ErrorMessageResourceName = "NumericLessThanOrEqual", 
                ErrorMessageResourceType = typeof(Validation))]
        [Integer(ErrorMessageResourceName = "Integer", ErrorMessageResourceType = typeof(Validation))]
        public virtual int? WheelchairsNum { get; set; }


        [DataType(DataType.MultilineText)]
        [Display(Name = "ModelComments", ResourceType = typeof(Drivers))]
        public virtual string Comments { get; set; }
        #endregion
    }
}