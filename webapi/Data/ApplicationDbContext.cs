using Microsoft.EntityFrameworkCore;
using webapi.Data.Model;

namespace webapi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships, indexes, and constraints here if needed.
             modelBuilder.Entity<Event>().HasIndex(p => p.Name);
        }
    }
}
