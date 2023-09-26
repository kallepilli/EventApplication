using Microsoft.EntityFrameworkCore;
using webapi.Data.Model;

namespace webapi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<EventParticipant> EventParticipants { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                .HasIndex(e => e.Name);

            modelBuilder.Entity<Event>()
                .HasIndex(e => e.Id);

            modelBuilder.Entity<EventParticipant>()
                .HasKey(ep => new { ep.EventId, ep.Id });

            modelBuilder.Entity<EventParticipant>()
                .HasOne(ep => ep.Event)
                .WithMany(e => e.EventParticipants)
                .HasForeignKey(ep => ep.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EventParticipant>()
                .HasIndex(ep => ep.IdCode);

            modelBuilder.Entity<EventParticipant>()
                .HasIndex(ep => new { ep.FirstName, ep.LastName, ep.CompanyName })
                .IsUnique(false);

            modelBuilder.Entity<EventParticipant>()
                .HasIndex(ep => ep.Id);
        }
    }
}
