using Microsoft.EntityFrameworkCore;
using RebelParkrunCup.Shared;

namespace RebelParkrunCup.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Runner> Runners { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Competitor> Competitors { get; set; }
        public DbSet<Location> Locations { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships.
            modelBuilder.Entity<Competitor>()
                .HasOne(bt => bt.Runner)
                .WithMany(r => r.Competitors)
                .HasForeignKey(bt => bt.RunnerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Competitor>()
                .HasOne(bt => bt.Tournament)
                .WithMany(t => t.Competitors)
                .HasForeignKey(bt => bt.TournamentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}