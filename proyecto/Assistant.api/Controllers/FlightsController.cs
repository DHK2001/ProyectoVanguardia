using Assistant.Api.DataTransferObjects;
using Assistant.Core.Entities;
using Assistant.Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static FlightsController;
using Assistant.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightsController : AssistantBaseController
{
    private readonly IRepository<Flight> flightRepository;
    private readonly IRepository<FlightStatus> flightStatusRepository;
    private readonly IRepository<FlightReservation> flightReservationRepository;

    public FlightsController(
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
    public async Task<IActionResult> GetFlights([FromQuery] string? flightNumber, [FromQuery] string? arrivalAirport, [FromQuery] string? departureAirport, [FromQuery] DateTime? flightDate)
    {
        if (string.IsNullOrEmpty(flightNumber) && string.IsNullOrEmpty(arrivalAirport) && string.IsNullOrEmpty(departureAirport) && flightDate == null)
        {
            var flights = await flightRepository.AllAsync();
            var flightDtos = flights.Select(flight =>
            {
                var flightStatus = flightStatusRepository.Filter(fs => fs.FlightId == flight.Id).FirstOrDefault();

                return new FlightDetailDataTransferObject
                {
                    Id = flight.Id,
                    FlightNumber = flight.FlightNumber,
                    DepartureAirport = flight.DepartureAirport,
                    ArrivalAirport = flight.ArrivalAirport,
                    FlightDate = flight.FlightDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    AirlineName = flight.AirlineName,
                    ScheduledDeparture = flightStatus?.ScheduledDeparture.ToString("yyyy-MM-dd HH:mm:ss"),
                    ScheduledArrival = flightStatus?.ScheduledArrival.ToString("yyyy-MM-dd HH:mm:ss")
                };
            }).ToList();

            return Ok(flightDtos);
        }
        else
        {
            var flights = flightRepository.Filter(f =>
                (string.IsNullOrEmpty(flightNumber) || f.FlightNumber == flightNumber) &&
                (string.IsNullOrEmpty(arrivalAirport) || f.ArrivalAirport == arrivalAirport) &&
                (string.IsNullOrEmpty(departureAirport) || f.DepartureAirport == departureAirport) &&
                (flightDate == null || f.FlightDate.Date == flightDate.Value.Date));

            if (!flights.Any())
                return NotFound("No se encontraron vuelos con los criterios de búsqueda proporcionados");

            var flightDtos = flights.Select(flight =>
            {
                var flightStatus = flightStatusRepository.Filter(fs => fs.FlightId == flight.Id).FirstOrDefault();

                return new FlightDetailDataTransferObject
                {
                    Id = flight.Id,
                    FlightNumber = flight.FlightNumber,
                    DepartureAirport = flight.DepartureAirport,
                    ArrivalAirport = flight.ArrivalAirport,
                    FlightDate = flight.FlightDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    AirlineName = flight.AirlineName,
                    ScheduledDeparture = flightStatus?.ScheduledDeparture.ToString("yyyy-MM-dd HH:mm:ss"),
                    ScheduledArrival = flightStatus?.ScheduledArrival.ToString("yyyy-MM-dd HH:mm:ss")
                };
            }).ToList();

            return Ok(flightDtos);
        }
    }


    [HttpGet("{flightid}")]
    public IActionResult GetById(int flightid)
    {
        var flight = flightRepository.GetById(flightid);
        if (flight == null)
            return NotFound($"El vuelo con el id '{flightid}' no existe");

        var flightStatus = flightStatusRepository.Filter(fs => fs.FlightId == flight.Id).FirstOrDefault();


        var flightDto = new FlightDetailDataTransferObject
        {
            Id = flight.Id,
            FlightNumber = flight.FlightNumber,
            DepartureAirport = flight.DepartureAirport,
            ArrivalAirport = flight.ArrivalAirport,
            FlightDate = flight.FlightDate.ToString("yyyy-MM-dd HH:mm:ss"),
            AirlineName = flight.AirlineName,
            ScheduledDeparture = flightStatus?.ScheduledDeparture.ToString("yyyy-MM-dd HH:mm:ss"),
            ScheduledArrival = flightStatus?.ScheduledArrival.ToString("yyyy-MM-dd HH:mm:ss")
        };

        return Ok(flightDto);
    }


    [HttpPost]
    public async Task<IActionResult> AddFlight(AddFlightDataTransferObject flightDto)
    {
        var flight = new Flight
        {
            FlightNumber = flightDto.FlightNumber,
            DepartureAirport = flightDto.DepartureAirport,
            ArrivalAirport = flightDto.ArrivalAirport,
            FlightDate = flightDto.FlightDate,
            AirlineName = flightDto.AirlineName,
        };

        var addedFlight = await flightRepository.AddAsync(flight);
        await flightRepository.CommitAsync();

        return Ok(addedFlight);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteReservation(int id)
    {
        var flight = flightRepository.GetById(id);
        if (flight == null)
        {
            return NotFound($"No se encontró el vuelo con el ID '{id}'.");
        }

        flightRepository.Delete(flight);
        flightRepository.CommitAsync();

        return Ok("Se elimino");
    }

}