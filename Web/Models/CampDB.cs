using System.Data.Entity;
using Camp.Interfaces;

namespace Camp.Models
{
    public class CampDB : DbContext, ICampDB
    {
		public CampDB(bool MultipleActiveResultSets = false) {
			//http://stackoverflow.com/questions/4867602/entity-framework-there-is-already-an-open-datareader-associated-with-this-comma
			this.Database
			.Connection
			.ConnectionString += (MultipleActiveResultSets ? ";MultipleActiveResultSets=true;" : "");
		}

		public IDbSet<DriverModel> Drivers { get; set; }
		public IDbSet<CamperModel> Campers { get; set; }
		public IDbSet<UserModel> Users { get; set; }
		public IDbSet<CampModel> Camps { get; set; }
		public IDbSet<CamperForYearModel> CampersForYear { get; set; }
    }
}