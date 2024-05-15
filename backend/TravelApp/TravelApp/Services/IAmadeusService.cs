using TravelApp.Services.Models;

namespace TravelApp.Services
{
    public interface IAmadeusService
    {
        Task<List<SearchResultModel>> GetFlightAsync(string departureAirport, string arrivalAirport, string dateFrom, string dateTo, string curency, int passengers, string token);
    }
}