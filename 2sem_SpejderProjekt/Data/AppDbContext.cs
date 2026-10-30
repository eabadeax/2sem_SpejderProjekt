
using Microsoft.EntityFrameworkCore;
using _2sem_SpejderProjekt.Models;
namespace _2sem_SpejderProjekt.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<Scout> Scouts { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<ScoutBadge> ScoutBadges { get; set; }
        public DbSet<Registration> Registrations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ScoutBadge>()
                .HasKey(b => b.BadgeId);


            modelBuilder.Entity<Registration>()
                .HasOne<Scout>()
                .WithMany()
                .HasForeignKey(r => r.ScoutId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Registration>()
                .HasOne<Activity>()
                .WithMany()
                .HasForeignKey(r => r.ActivityId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Registration>()
                .HasIndex(r => new { r.ScoutId, r.ActivityId })
                .IsUnique();

        }
    }
}
