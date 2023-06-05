using Assistant.Core.Entities;
using System;
namespace Assistant.Api.DataTransferObjects
{
	public class FlightDetailDataTransferObject
	{
        public int Id { get; set; }
        public string FlightNumber { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public String FlightDate { get; set; }
        public string AirlineName { get; set; }
        public string ScheduledDeparture { get; set; }
        public string ScheduledArrival { get; set; }

    }
}

