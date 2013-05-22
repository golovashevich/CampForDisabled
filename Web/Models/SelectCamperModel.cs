using System.Collections.Generic;

namespace Camp.Models
{
	public class SelectCamperModel
	{
		public virtual IEnumerable<CamperModel> AvailableCampers { get; set; } 
		public virtual IEnumerable<CamperModel> ChosenCampers { get; set; }
	}

	public class SelectCamperPostModel
	{
		public IEnumerable<int> ChosenCampers { get; set; }
	}
}