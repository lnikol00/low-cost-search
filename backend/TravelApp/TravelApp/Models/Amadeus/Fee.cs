using System.Text.Json.Serialization;

namespace TravelApp.Models.Amadeus
{
    public class Fee
    {
        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
