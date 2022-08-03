using FlightService.Entity;
using FlightService.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightService.Entity
{
    public class FlightDbContext : DbContext
    {
        public FlightDbContext() 
        { 

        }
        
        public FlightDbContext(DbContextOptions<FlightDbContext> options) : base(options)
        {

        }

        public DbSet<Flight> Flight { get; set; }

        public DbSet<AirLineModel> AirLine { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("AppDb");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
