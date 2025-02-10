using Microsoft.EntityFrameworkCore;
using RebelParkrunCup.Shared;

namespace RebelParkrunCup.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Runner> Runners { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<BaselineTime> BaselineTimes { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships.
            modelBuilder.Entity<BaselineTime>()
                .HasOne(bt => bt.Runner)
                .WithMany(r => r.BaselineTimes)
                .HasForeignKey(bt => bt.RunnerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BaselineTime>()
                .HasOne(bt => bt.Tournament)
                .WithMany(t => t.BaselineTimes)
                .HasForeignKey(bt => bt.TournamentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}