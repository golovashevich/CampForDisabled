using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Camp.Models {
	[Table("CamperForYear")]
	public class CamperForYearModel {
		
		#region Public properties
		[HiddenInput]
		public virtual int Id { get; set; }
		
		[ForeignKey("Camp")]
		public virtual int CampId { get; set; }
		public virtual CampModel Camp { get; set; }

		[ForeignKey("Camper")]
		public virtual int CamperId { get; set; }
		public virtual CamperModel Camper { get; set; }

		#endregion
	}
}