using System.Text.Json.Serialization;

namespace TravelApp.Models.Amadeus
{
    public class Carriers
    {
        [JsonPropertyName("PR")]
        public string PR { get; set; }
    }
}
