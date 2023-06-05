using Assistant.Core.Entities;
using System;
namespace Assistant.Api.DataTransferObjects
{
	public class ReservationDetailDataTransferObject
	{
        public int Id { get; set; }
        public string PassengerName { get; set; }
        public int NumberOfPassengers { get; set; }

        public string FlightNumber { get; set; }
    }
}

