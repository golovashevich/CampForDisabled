using System.Data.Entity;
using Camp.Interfaces;

namespace Camp.Models
{
    public class CampDB : DbContext, ICampDB
    {
        public DbSet<DriverModel> Drivers { get; set; }
        public DbSet<CamperModel> Campers { get; set; }
        public DbSet<UserModel> Users { get; set; }
		public DbSet<CampModel> Camps { get; set; }
    }
}