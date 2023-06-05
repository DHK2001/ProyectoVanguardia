using Assistant.Api.DataTransferObjects;
using Assistant.Core.Entities;
using Assistant.Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static FlightsController;
using Assistant.Api.Controllers;
using System.Text.Json.Serialization;
using System.Text.Json;

[ApiController]
[Route("[controller]")]
public class ReservationsController : AssistantBaseController
{
    private readonly IRepository<Flight> flightRepository;
    private readonly IRepository<FlightStatus> flightStatusRepository;
    private readonly IRepository<FlightReservation> flightReservationRepository;

    public ReservationsController(
        IRepository<Flight> flightRepository,
        IRepository<FlightStatus> flightStatusRepository,
        IRepository<FlightReservation> flightReservationRepository
    )
    {
        this.flightRepository = flightRepository;
        this.flightStatusRepository = flightStatusRepository;
        this.flightReservationRepository = flightReservationRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllReservations([FromQuery] string? passengerName)
    {
        var reservations = flightReservationRepository.Filter(r =>
            passengerName == null || r.PassengerName == passengerName);

        var reservationDtos = reservations.Select(reservation => new ReservationDetailDataTransferObject
        {
            Id = reservation.Id,
            PassengerName = reservation.PassengerName,
            NumberOfPassengers = reservation.NumberOfPassengers,
            FlightNumber = flightRepository.GetById(reservation.FlightId).FlightNumber
        }).ToList();

        if (passengerName != null && !reservationDtos.Any())
        {
            return NotFound("No se encontraron reservaciones.");
        }

        return Ok(reservationDtos);
    }

    [HttpGet("{id}")]
    public IActionResult GetReservationById(int id)
    {
        var reservation = flightReservationRepository.GetById(id);
        var flight = flightRepository.GetById(reservation.FlightId);

        if (reservation == null)
        {
            return NotFound($"No se encontró la reserva con el ID '{id}'.");
        }

        var reservationDto = new ReservationDetailDataTransferObject
        {
            Id = reservation.Id,
            PassengerName = reservation.PassengerName,
            NumberOfPassengers = reservation.NumberOfPassengers,
            FlightNumber = flight.FlightNumber
        };

        return Ok(reservationDto);
    }



    [HttpPost("reserve")]
    public async Task<IActionResult> ReserveFlight(AddReservationDataTransferObject reservationDto)
    {
        var flights = flightRepository.Filter(f => f.FlightNumber == reservationDto.FlightNumber);
        var flight = flights.FirstOrDefault();
        if (flight == null)
            return NotFound($"El vuelo con el número '{reservationDto.FlightNumber}' no existe");

        // Verificar si ya existe una reserva con el mismo nombre de pasajero y el mismo viaje
        var existingReservation = flightReservationRepository.Filter(r =>
            r.PassengerName == reservationDto.PassengerName &&
            r.FlightId == flight.Id).FirstOrDefault();


        if (existingReservation != null)
        {
            return Conflict($"Ya existe una reserva para el pasajero '{reservationDto.PassengerName}' en este vuelo");
        }

        var flightReservation = new FlightReservation
        {
            PassengerName = reservationDto.PassengerName,
            NumberOfPassengers = reservationDto.NumberOfPassengers,
            FlightId = flight.Id,
        };

        var addedReservation = await flightReservationRepository.AddAsync(flightReservation);
        await flightReservationRepository.CommitAsync();

        return Ok(reservationDto);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteReservation(int id)
    {
        var reservation = flightReservationRepository.GetById(id);
        if (reservation == null)
        {
            return NotFound($"No se encontró la reserva con el ID '{id}'.");
        }

        flightReservationRepository.Delete(reservation);
        flightReservationRepository.CommitAsync();

        return Ok("Se elimino");
    }


}