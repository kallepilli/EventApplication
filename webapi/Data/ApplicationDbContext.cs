using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
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


            //modelBuilder.Entity<Event>().HasData(
            //new Event
            //{
            //    Id = "test1",
            //    Name = "Konverents",
            //    EventTime = DateTime.Today.AddDays(-3).ToUniversalTime(),
            //    Location = "Tallinn",
            //    AdditionalInfo = "Investor Toomase konverents"
            //},
            //new Event
            //{
            //    Id = "test2",
            //    Name = "Ettevõtte pidu",
            //    EventTime = DateTime.Today.AddDays(-1).ToUniversalTime(),
            //    Location = "Pärnu",
            //    AdditionalInfo = "Open bar"
            //},
            //new Event
            //{
            //    Id = "test3",
            //    Name = "Automess",
            //    EventTime = DateTime.Today.ToUniversalTime(),
            //    Location = "Tartu",
            //    AdditionalInfo = "Luksuslikud autod"
            //},
            //new Event
            //{
            //    Id = "test4",
            //    Name = "Etendus",
            //    EventTime = DateTime.Today.AddDays(10).ToUniversalTime(),
            //    Location = "Viljandi",
            //    AdditionalInfo = "Head näitlejad"
            //});

            //modelBuilder.Entity<EventParticipant>().HasData(
            //new EventParticipant
            //{
            //    EventId = "test1",
            //    IsCompany = false,
            //    FirstName = "Kalle",
            //    LastName = "Pilli",
            //    IdCode = "39208014225",
            //    AdditionalInfo = "Sooviks hotellituba",
            //    PaymentMethod = PaymentMethod.Cash
            //},
            //new EventParticipant
            //{
            //    EventId = "test1",
            //    IsCompany = false,
            //    FirstName = "Kristjan",
            //    LastName = "Liivamägi",
            //    IdCode = "36308014226",
            //    AdditionalInfo = "",
            //    PaymentMethod = PaymentMethod.BankTransfer
            //},
            //new EventParticipant
            //{
            //    EventId = "test1",
            //    IsCompany = true,
            //    CompanyName = "Tallinna Kaubamaja AS",
            //    ParticipantCount = 12,
            //    IdCode = "12349876",
            //    AdditionalInfo = "Ühel inimesel on koer kaasas",
            //    PaymentMethod = PaymentMethod.BankTransfer
            //},
            //new EventParticipant
            //{
            //    EventId = "test1",
            //    IsCompany = true,
            //    CompanyName = "Desperado Ehitus OÜ",
            //    ParticipantCount = 2,
            //    IdCode = "98765432",
            //    AdditionalInfo = "Tiit ja Teet teevad ka ettekande",
            //    PaymentMethod = PaymentMethod.Cash
            //}
            );
        }
    }
}
