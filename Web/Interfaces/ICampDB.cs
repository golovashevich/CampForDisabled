using System.Data.Entity;
using Camp.Models;

namespace Camp.Interfaces
{
    public interface ICampDB
    {
		IDbSet<CamperModel> Campers { get; set; }
		IDbSet<CampModel> Camps { get; set; }
		IDbSet<DriverModel> Drivers { get; set; }
		IDbSet<UserModel> Users { get; set; }
		IDbSet<CamperForYearModel> CampersForYear { get; set; }

        int SaveChanges();
    }
}
