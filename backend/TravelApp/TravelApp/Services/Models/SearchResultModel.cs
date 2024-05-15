namespace TravelApp.Services.Models
{
    public class SearchResultModel
    {
        public string? DepartureAirportName { get; set; }
        public string? ArrivalAirportName { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int DepartureTransfers { get; set; }
        public int? ReturnTransfers { get; set; }
        public int Passengers { get; set; }
        public string? Currency { get; set; }
        public string? Price { get; set; }
    }
}
