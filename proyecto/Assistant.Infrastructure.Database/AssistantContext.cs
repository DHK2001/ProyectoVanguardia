using Assistant.Core.Entities;
using Assistant.Infrastructure.Database.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Assistant.Infrastructure.Database
{
    public class AssistantContext 
        : DbContext
    {
        public AssistantContext(DbContextOptions<AssistantContext> options)
            : base(options)
        {

        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<FlightStatus> FlightStatus { get; set; }
        public DbSet<FlightReservation> FlightReservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FlightsConfiguration());
            modelBuilder.ApplyConfiguration(new FlightStatusConfiguration());
            modelBuilder.ApplyConfiguration(new FlightReservationConfiguration());
        }
    }
}