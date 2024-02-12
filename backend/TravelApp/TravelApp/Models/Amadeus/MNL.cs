using System.Text.Json.Serialization;

namespace TravelApp.Models.Amadeus
{
    public class MNL
    {
        [JsonPropertyName("cityCode")]
        public string CityCode { get; set; }

        [JsonPropertyName("countryCode")]
        public string CountryCode { get; set; }
    }
}
