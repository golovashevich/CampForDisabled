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
		
		public DbSet<DriverModel> Drivers { get; set; }
        public DbSet<CamperModel> Campers { get; set; }
        public DbSet<UserModel> Users { get; set; }
		public DbSet<CampModel> Camps { get; set; }
		public DbSet<CamperForYearModel> CampersForYear { get; set; }
    }
}