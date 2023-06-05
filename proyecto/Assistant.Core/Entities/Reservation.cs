namespace Assistant.Core.Entities
{
    public class FlightReservation
    {
        public int Id { get; set; }
        public string PassengerName { get; set; }
        public int NumberOfPassengers { get; set; }

        public int FlightId { get; set; }
        public Flight Flight { get; set; }
    }
}
