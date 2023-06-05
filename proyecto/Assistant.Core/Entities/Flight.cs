namespace Assistant.Core.Entities
{
    public class Flight
    {
        public int Id { get; set; }
        public string FlightNumber { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public DateTime FlightDate { get; set; }
        public string AirlineName { get; set; }

        public ICollection<FlightReservation> FlightReservations { get; set; }
        public FlightStatus FlightStatus { get; set; }
    }
}
