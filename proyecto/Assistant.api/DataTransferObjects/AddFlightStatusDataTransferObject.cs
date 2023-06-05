using Assistant.Core.Entities;
using System;
namespace Assistant.Api.DataTransferObjects
{
    public class AddFlightStatusDataTransferObject
    {
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public DateTime ScheduledDeparture { get; set; }
        public DateTime ScheduledArrival { get; set; }
    }
}

