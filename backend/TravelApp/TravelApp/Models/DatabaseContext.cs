using Microsoft.EntityFrameworkCore;

namespace TravelApp.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) 
        { 
        }

        public DbSet<Airport> Airports { get; set; }
        public DbSet<SearchParams> Searches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SearchParams>().HasKey(nameof(SearchParams.DepartureAirport), nameof(SearchParams.ArrivalAirport), nameof(SearchParams.DepartureDate), nameof(SearchParams.Currency), nameof(SearchParams.Passengers), nameof(SearchParams.ReturnDate));
            modelBuilder.Entity<SearchParams>().Property(x => x.Currency).HasConversion<string>();


            base.OnModelCreating(modelBuilder);
        }
    }
}
