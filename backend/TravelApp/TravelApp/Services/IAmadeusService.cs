using TravelApp.Controllers.DTO;
using TravelApp.Services.Models;

namespace TravelApp.Services
{
    public interface IAmadeusService
    {
        Task<List<SearchResultModel>> GetFlightAsync(SearchRequestDTO planes, string token);

        Task<List<Airport>> AllAirports();
    }
}