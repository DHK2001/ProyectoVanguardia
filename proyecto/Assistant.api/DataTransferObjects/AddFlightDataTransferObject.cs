using Assistant.Core.Entities;
using System;
namespace Assistant.Api.DataTransferObjects
{
	public class AddFlightDataTransferObject
	{
        public string FlightNumber { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public DateTime FlightDate { get; set; }
        public string AirlineName { get; set; }
    }
}

