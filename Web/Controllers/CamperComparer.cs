using System.Collections.Generic;
using Camp.Models;

namespace Web.Controllers
{
	public class CamperComparer : IEqualityComparer<CamperModel> {
		public int GetHashCode(CamperModel camper) {
			if (camper == null) {
				return 0;
			}
			return camper.Id.GetHashCode();
		}

		public bool Equals(CamperModel x1, CamperModel x2) {
			if (object.ReferenceEquals(x1, x2)) {
				return true;
			}
			if (object.ReferenceEquals(x1, null) ||
				object.ReferenceEquals(x2, null)) {
				return false;
			}
			return x1.Id == x2.Id;
		}
	}
}