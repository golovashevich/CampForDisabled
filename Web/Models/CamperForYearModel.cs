using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Camp.Models {
	[Table("CamperForYear")]
	public class CamperForYearModel {
		
		#region Public properties
		[HiddenInput]
		public virtual int Id { get; set; }
		
		//[ForeignKey("YearId")]
		public virtual int YearId { get; set; }

		//[ForeignKey("CamperId")]
		public virtual int CamperId { get; set; }

		#endregion
	}
}