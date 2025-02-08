using Microsoft.EntityFrameworkCore;
using RebelParkrunCup.Shared;

namespace RebelParkrunCup.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Runner> Runners { get; set; }
    }
}