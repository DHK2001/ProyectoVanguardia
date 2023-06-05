﻿// <auto-generated />
using System;
using Assistant.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Assistant.Infrastructure.Database.Migrations
{
    [DbContext(typeof(AssistantContext))]
    partial class AssistantContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("Assistant.Core.Entities.Flight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AirlineName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ArrivalAirport")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DepartureAirport")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FlightDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("FlightNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("Assistant.Core.Entities.FlightReservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("FlightId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NumberOfPassengers")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PassengerName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FlightId");

                    b.ToTable("FlightReservations");
                });

            modelBuilder.Entity("Assistant.Core.Entities.FlightStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ArrivalAirport")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DepartureAirport")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("FlightId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FlightNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ScheduledArrival")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ScheduledDeparture")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FlightId")
                        .IsUnique();

                    b.ToTable("FlightStatus");
                });

            modelBuilder.Entity("Assistant.Core.Entities.FlightReservation", b =>
                {
                    b.HasOne("Assistant.Core.Entities.Flight", "Flight")
                        .WithMany("FlightReservations")
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Flight");
                });

            modelBuilder.Entity("Assistant.Core.Entities.FlightStatus", b =>
                {
                    b.HasOne("Assistant.Core.Entities.Flight", "Flight")
                        .WithOne("FlightStatus")
                        .HasForeignKey("Assistant.Core.Entities.FlightStatus", "FlightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Flight");
                });

            modelBuilder.Entity("Assistant.Core.Entities.Flight", b =>
                {
                    b.Navigation("FlightReservations");

                    b.Navigation("FlightStatus")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
