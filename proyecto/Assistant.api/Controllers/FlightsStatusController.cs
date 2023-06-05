using Assistant.Api.DataTransferObjects;
using Assistant.Core.Entities;
using Assistant.Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Assistant.Api.Controllers;
using System;

[ApiController]

public class FlightsStatusController : AssistantBaseController
{
    private readonly IRepository<Flight> flightRepository;
    private readonly IRepository<FlightStatus> flightStatusRepository;
    private readonly IRepository<FlightReservation> flightReservationRepository;

    public FlightsStatusController(
        IRepository<Flight> flightRepository,
        IRepository<FlightStatus> flightStatusRepository,
        IRepository<FlightReservation> flightReservationRepository
    )
    {
        this.flightRepository = flightRepository;
        this.flightStatusRepository = flightStatusRepository;
        this.flightReservationRepository = flightReservationRepository;
    }

    [HttpGet("flight/status")]
    public async Task<IActionResult> GetAllAsync()
    {
        var flightStatusList = await flightStatusRepository.AllAsync();
        var flightStatusDtos = flightStatusList.Select(flightStatus => new FlightStatusDetailDataTransferObject
        {
            Id = flightStatus.Id,
            FlightNumber = flightStatus.FlightNumber,
            DepartureAirport = flightStatus.DepartureAirport,
            ArrivalAirport = flightStatus.ArrivalAirport,
            ScheduledDeparture = flightStatus.ScheduledDeparture.ToString("yyyy-MM-dd HH:mm:ss"),
            ScheduledArrival = flightStatus.ScheduledArrival.ToString("yyyy-MM-dd HH:mm:ss"),
            FlightId = flightStatus.FlightId
        }).ToList();

        return Ok(flightStatusDtos);
    }

    [HttpGet("flight/status/{id}")]
    public IActionResult GetFlightStatusById(int id)
    {
        var flightStatus = flightStatusRepository.GetById(id);
        if (flightStatus == null)
            return NotFound($"El estatus de vuelo con el ID '{id}' no existe");

        var flightStatusDto = new FlightStatusDetailDataTransferObject
        {
            Id = flightStatus.Id,
            FlightNumber = flightStatus.FlightNumber,
            DepartureAirport = flightStatus.DepartureAirport,
            ArrivalAirport = flightStatus.ArrivalAirport,
            ScheduledDeparture = flightStatus.ScheduledDeparture.ToString("yyyy-MM-dd HH:mm:ss"),
            ScheduledArrival = flightStatus.ScheduledArrival.ToString("yyyy-MM-dd HH:mm:ss"),
            FlightId = flightStatus.FlightId
        };

        return Ok(flightStatusDto);
    }

    [HttpGet("flight/{flightNumber}/status")]
    public IActionResult GetFlightStatus(string flightNumber)
    {
        var flightStatus = flightStatusRepository.Filter(fs => fs.FlightNumber == flightNumber).FirstOrDefault();
        if (flightStatus == null)
            return NotFound($"El estatus de vuelo con el número '{flightNumber}' no existe");

        var flightStatusDto = new FlightStatusDetailDataTransferObject
        {
            Id = flightStatus.Id,
            FlightNumber = flightStatus.FlightNumber,
            DepartureAirport = flightStatus.DepartureAirport,
            ArrivalAirport = flightStatus.ArrivalAirport,
            ScheduledDeparture = flightStatus.ScheduledDeparture.ToString("yyyy-MM-dd HH:mm:ss"),
            ScheduledArrival = flightStatus.ScheduledArrival.ToString("yyyy-MM-dd HH:mm:ss"),
            FlightId = flightStatus.FlightId
        };

        return Ok(flightStatusDto);
    }

    [HttpPost("flight/{flightNumber}/status")]
    public async Task<IActionResult> AddFlightStatus(string flightNumber, AddFlightStatusDataTransferObject flightStatusDto)
    {
        var flights = flightRepository.Filter(f => f.FlightNumber == flightNumber);
        var flight = flights.FirstOrDefault();
        if (flight == null)
            return NotFound($"El vuelo con el número '{flightNumber}' no existe");

        var flightStatus = new FlightStatus
        {
            FlightNumber = flightNumber,
            DepartureAirport = flightStatusDto.DepartureAirport,
            ArrivalAirport = flightStatusDto.ArrivalAirport,
            ScheduledDeparture = flightStatusDto.ScheduledDeparture,
            ScheduledArrival = flightStatusDto.ScheduledArrival,
            FlightId = flight.Id
        };

        var addedFlightStatus = await flightStatusRepository.AddAsync(flightStatus);
        await flightStatusRepository.CommitAsync();

        return Ok(flightStatusDto);
    }

    [HttpDelete("flight/status/{id}")]
    public IActionResult DeleteReservation(int id)
    {
        var flightStatus = flightStatusRepository.GetById(id);
        if (flightStatus == null)
        {
            return NotFound($"No se encontró el status con el ID '{id}'.");
        }

        flightStatusRepository.Delete(flightStatus);
        flightStatusRepository.CommitAsync();

        return Ok("Se elimino");
    }

}