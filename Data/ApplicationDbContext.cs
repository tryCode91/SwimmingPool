using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SwimmingPool_V1.Models;

namespace SwimmingPool_V1.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Pool> Pool { get; set; }
        public DbSet<Lane> Lane { get; set; }
        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<Reservation> Reservation { get; set; }

    }
}
