using Assistant.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assistant.Infrastructure.Database.Configuration
{
    public class FlightStatusConfiguration : IEntityTypeConfiguration<FlightStatus>
    {
        public void Configure(EntityTypeBuilder<FlightStatus> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FlightNumber).IsRequired();
            builder.Property(x => x.DepartureAirport).IsRequired();
            builder.Property(x => x.ArrivalAirport).IsRequired();

            builder.HasOne(x => x.Flight)
                .WithOne(x => x.FlightStatus)
                .HasForeignKey<FlightStatus>(x => x.FlightId)
                .IsRequired();
        }
    }
}



