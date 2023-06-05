namespace Assistant.Core.Entities
{
    public class FlightStatus
    {
        public int Id { get; set; }
        public string FlightNumber { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public DateTime ScheduledDeparture { get; set; }
        public DateTime ScheduledArrival { get; set; }
        public int FlightId { get; set; }
        public Flight Flight { get; set; }
    }
}

