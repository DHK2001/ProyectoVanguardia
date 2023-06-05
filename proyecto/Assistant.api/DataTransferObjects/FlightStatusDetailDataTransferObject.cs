﻿using Assistant.Core.Entities;
using System;
namespace Assistant.Api.DataTransferObjects
{
	public class FlightStatusDetailDataTransferObject
	{
        public int Id { get; set; }
        public string FlightNumber { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public string ScheduledDeparture { get; set; }
        public string ScheduledArrival { get; set; }
        public int FlightId { get; set; }
    }
}

