using System.Text.Json.Serialization;

namespace TravelApp.Models.Amadeus
{
    public class Links
    {
        [JsonPropertyName("self")]
        public string Self { get; set; }
    }
}
