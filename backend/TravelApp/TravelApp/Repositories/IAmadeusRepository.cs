namespace TravelApp.Repositories
{
    public interface IAmadeusRepository
    {
        Task<string> GetFlightAsync(string origin, string destination, string departureDate, string returnDate, string currency, int passengers, string token);
    }
}