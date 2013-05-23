using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Resources;

namespace Camp.Models
{
	public class SelectCamperModel
	{
		[Display(Name = "ModelChosenCampers", ResourceType = typeof(CamperForYear))]
		public virtual IEnumerable<CamperModel> ChosenCampers { get; set; }

		[Display(Name = "ModelAvailableCampers", ResourceType = typeof(CamperForYear))]
		public virtual IEnumerable<CamperModel> AvailableCampers { get; set; } 
	}

	public class SelectCamperPostModel
	{
		public IEnumerable<int> ChosenCampers { get; set; }
	}
}