using TravelApp.Services.Models;

namespace TravelApp.Controllers.DTO
{
    public class SearchRequestDTO
    {
        public string departureAirport { get; set; }
        public string arrivalAirport { get; set; }
        public string dateFrom { get; set; }
        public string dateTo { get; set; }
        public string curency { get; set; }
        public int passengers { get; set; }

        public SearchParams ToModel()
        {
            return new SearchParams
            {
                DepartureAirport = departureAirport,
                ArrivalAirport = arrivalAirport,
                DepartureDate = DateTime.Parse(dateFrom),
                ReturnDate = DateTime.Parse(dateTo),
                Currency = (Currency)Enum.Parse(typeof(Currency), curency),
                Passengers = passengers
            };
        }
    }
}
