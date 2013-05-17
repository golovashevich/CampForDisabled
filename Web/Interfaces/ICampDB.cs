using System.Data.Entity;
using Camp.Models;

namespace Camp.Interfaces
{
    public interface ICampDB
    {
        DbSet<CamperModel> Campers { get; set; }
        DbSet<CampModel> Camps { get; set; }
        DbSet<DriverModel> Drivers { get; set; }
        DbSet<UserModel> Users { get; set; }
		DbSet<CamperForYearModel> CampersForYear { get; set; }

        int SaveChanges();
    }
}
