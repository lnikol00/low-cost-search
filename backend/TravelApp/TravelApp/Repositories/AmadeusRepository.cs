using System.Net.Http.Headers;

namespace TravelApp.Repositories
{
    public class AmadeusRepository : IAmadeusRepository
    {
        public async Task<string> GetFlightAsync(string origin, string destination, string departureDate, string returnDate, string currency, int passengers, string token)
        {
            string url = $"https://test.api.amadeus.com/v2/shopping/flight-offers?originLocationCode={origin}&destinationLocationCode={destination}&departureDate={departureDate}&adults={passengers}&currencyCode={currency}";

            if (!string.IsNullOrEmpty(returnDate))
            {
                url += $"$returnDate={returnDate}";
            }

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            using var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return "";
        }
    }
}
