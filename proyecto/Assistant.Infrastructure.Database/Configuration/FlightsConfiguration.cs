using Assistant.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assistant.Infrastructure.Database.Configuration
{
    public class FlightsConfiguration : IEntityTypeConfiguration<Flight>
    {
        public void Configure(EntityTypeBuilder<Flight> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FlightNumber).IsRequired();
            builder.Property(x => x.DepartureAirport).IsRequired();
            builder.Property(x => x.ArrivalAirport).IsRequired();
            builder.Property(x => x.FlightDate).IsRequired();
            builder.Property(x => x.AirlineName).IsRequired();

            builder.HasOne(f => f.FlightStatus)
                .WithOne()
                .HasForeignKey<FlightStatus>(f => f.FlightId)
                .IsRequired();
        }
    }
}




