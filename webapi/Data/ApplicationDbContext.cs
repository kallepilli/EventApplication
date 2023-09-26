using Microsoft.EntityFrameworkCore;
using webapi.Data.Model;

namespace webapi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Event> EventParticipants { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<EventParticipant>()
                .HasKey(ep => new { ep.EventId, ep.ParticipantId });

            modelBuilder.Entity<EventParticipant>()
                .HasOne(ep => ep.Event)
                .WithMany()
                .HasForeignKey(ep => ep.EventId);

            modelBuilder.Entity<EventParticipant>()
                .HasOne(ep => ep.Participant)
                .WithMany()
                .HasForeignKey(ep => ep.ParticipantId);

            modelBuilder.Entity<Participant>()
                .HasIndex(p => p.IdCode);

            modelBuilder.Entity<Participant>()
                .HasIndex(p => p.Id);

            modelBuilder.Entity<Participant>()
                .Property(p => p.PaymentMethod)
                .HasConversion<string>();

            modelBuilder.Entity<Event>()
                .HasIndex(e => e.Name);

            modelBuilder.Entity<Event>()
                .HasIndex(e => e.Id);
        }
    }
}
