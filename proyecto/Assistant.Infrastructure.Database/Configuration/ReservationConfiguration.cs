using Assistant.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assistant.Infrastructure.Database.Configuration
{
    public class FlightReservationConfiguration : IEntityTypeConfiguration<FlightReservation>
    {
        public void Configure(EntityTypeBuilder<FlightReservation> builder)
        {
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();
            builder.HasKey(x => x.Id);

            builder.Property(x => x.PassengerName).IsRequired();
            builder.Property(x => x.NumberOfPassengers).IsRequired();

            builder.HasOne(x => x.Flight)
                .WithMany(x => x.FlightReservations)
                .HasForeignKey(x => x.FlightId)
                .IsRequired();
        }
    }
}

