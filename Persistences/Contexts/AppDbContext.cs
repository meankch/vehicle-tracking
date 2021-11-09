using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using vehicle_tracking.Models;

namespace vehicle_tracking.Persistences.Contexts
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<UserRole> UserRolesDb { get; set; }
        public DbSet<User> UsersDb { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleDevice> VehicleDevices { get; set; }
        public DbSet<Location> Locations { get; set; }
    }
}
