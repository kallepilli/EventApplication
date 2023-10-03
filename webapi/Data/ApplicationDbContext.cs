using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using webapi.Data.Model;

namespace webapi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<EventParticipant> EventParticipants { get; set; }
        public DbSet<Participant> Participants { get; set; }

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
                .HasKey(ep => new { ep.Id, ep.EventId, ep.ParticipantId });

            modelBuilder.Entity<EventParticipant>()
                .HasOne(ep => ep.Event)
                .WithMany(e => e.EventParticipants)
                .HasForeignKey(ep => ep.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EventParticipant>()
            .HasOne(ep => ep.Participant)
            .WithMany(e => e.EventParticipants)
            .HasForeignKey(ep => ep.ParticipantId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Participant>()
                .HasIndex(e => e.IdCode)
                .IsUnique(true);

            modelBuilder.Entity<Participant>()
                .HasIndex(ep => new { ep.FirstName, ep.LastName, ep.CompanyName })
                .IsUnique(false);

            modelBuilder.Entity<Participant>()
                .HasIndex(p => p.Id);


            modelBuilder.Entity<Event>().HasData(
            new Event
            {
                Id = "test1",
                Name = "Konverents",
                EventTime = DateTime.Today.AddDays(-3).ToUniversalTime(),
                Location = "Tallinn",
                AdditionalInfo = "Investor Toomase konverents"
            },
            new Event
            {
                Id = "test2",
                Name = "Ettevõtte pidu",
                EventTime = DateTime.Today.AddDays(-1).ToUniversalTime(),
                Location = "Pärnu",
                AdditionalInfo = "Open bar"
            },
            new Event
            {
                Id = "test3",
                Name = "Automess",
                EventTime = DateTime.Today.ToUniversalTime(),
                Location = "Tartu",
                AdditionalInfo = "Luksuslikud autod"
            },
            new Event
            {
                Id = "test4",
                Name = "Etendus",
                EventTime = DateTime.Today.AddDays(10).ToUniversalTime(),
                Location = "Viljandi",
                AdditionalInfo = "Head näitlejad"
            });

            modelBuilder.Entity<Participant>().HasData(
            new Participant
            {
                Id = "testParticipant1",
                IsCompany = false,
                FirstName = "Kalle",
                LastName = "Pilli",
                IdCode = "39208014225"
            },
            new Participant
            {
                Id = "testParticipant2",
                IsCompany = false,
                FirstName = "Investor",
                LastName = "Toomas",
                IdCode = "36308014226"
            },
            new Participant
            {
                Id = "testParticipant3",
                IsCompany = true,
                CompanyName = "Tallinna Kaubamaja AS",
                IdCode = "12349876",
            },
            new Participant
            {
                Id = "testParticipant4",
                IsCompany = true,
                CompanyName = "Desperado Ehitus OÜ",
                IdCode = "98765432",
            });

            modelBuilder.Entity<EventParticipant>().HasData(
            new EventParticipant
            {
                EventId = "test1",
                ParticipantId = "testParticipant1",
                AdditionalInfo = "Sooviks hotellituba",
                PaymentMethod = PaymentMethod.Cash
            },
            new EventParticipant
            {
                EventId = "test1",
                ParticipantId = "testParticipant2",
                AdditionalInfo = "",
                PaymentMethod = PaymentMethod.BankTransfer
            },
            new EventParticipant
            {
                EventId = "test1",
                ParticipantId = "testParticipant3",
                ParticipantCount = 12,
                AdditionalInfo = "Ühel inimesel on koer kaasas",
                PaymentMethod = PaymentMethod.BankTransfer
            },
            new EventParticipant
            {
                EventId = "test1",
                ParticipantId = "testParticipant4",
                ParticipantCount = 2,
                AdditionalInfo = "Tiit ja Teet teevad ka ettekande",
                PaymentMethod = PaymentMethod.Cash
            },
            new EventParticipant
            {
                EventId = "test2",
                ParticipantId = "testParticipant1",
                AdditionalInfo = "Sooviks hotellituba",
                PaymentMethod = PaymentMethod.Cash
            },
            new EventParticipant
            {
                EventId = "test2",
                ParticipantId = "testParticipant2",
                AdditionalInfo = "",
                PaymentMethod = PaymentMethod.BankTransfer
            },
            new EventParticipant
            {
                EventId = "test2",
                ParticipantId = "testParticipant3",
                ParticipantCount = 12,
                AdditionalInfo = "",
                PaymentMethod = PaymentMethod.BankTransfer
            },
            new EventParticipant
            {
                EventId = "test2",
                ParticipantId = "testParticipant4",
                ParticipantCount = 2,
                AdditionalInfo = "",
                PaymentMethod = PaymentMethod.Cash
            },
            new EventParticipant
            {
                EventId = "test3",
                ParticipantId = "testParticipant1",
                AdditionalInfo = "Sooviks hotellituba",
                PaymentMethod = PaymentMethod.Cash
            },
            new EventParticipant
            {
                EventId = "test3",
                ParticipantId = "testParticipant2",
                AdditionalInfo = "",
                PaymentMethod = PaymentMethod.BankTransfer
            },
            new EventParticipant
            {
                EventId = "test3",
                ParticipantId = "testParticipant3",
                ParticipantCount = 12,
                AdditionalInfo = "",
                PaymentMethod = PaymentMethod.BankTransfer
            },
            new EventParticipant
            {
                EventId = "test3",
                ParticipantId = "testParticipant4",
                ParticipantCount = 2,
                AdditionalInfo = "",
                PaymentMethod = PaymentMethod.Cash
            },
            new EventParticipant
            {
                EventId = "test4",
                ParticipantId = "testParticipant1",
                AdditionalInfo = "Sooviks hotellituba",
                PaymentMethod = PaymentMethod.Cash
            },
            new EventParticipant
            {
                EventId = "test4",
                ParticipantId = "testParticipant2",
                AdditionalInfo = "",
                PaymentMethod = PaymentMethod.BankTransfer
            },
            new EventParticipant
            {
                EventId = "test4",
                ParticipantId = "testParticipant3",
                ParticipantCount = 12,
                AdditionalInfo = "",
                PaymentMethod = PaymentMethod.BankTransfer
            },
            new EventParticipant
            {
                EventId = "test4",
                ParticipantId = "testParticipant4",
                ParticipantCount = 2,
                AdditionalInfo = "",
                PaymentMethod = PaymentMethod.Cash
            }
            );
        }
    }
}
