using System.Text.Json.Serialization;

namespace TravelApp.Models.Amadeus
{
    public class Currencies
    {
        [JsonPropertyName("EUR")]
        public string EUR { get; set; }
    }
}
