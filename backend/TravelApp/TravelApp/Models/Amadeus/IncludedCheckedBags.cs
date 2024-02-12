using System.Text.Json.Serialization;

namespace TravelApp.Models.Amadeus
{
    public class IncludedCheckedBags
    {
        [JsonPropertyName("weight")]
        public int Weight { get; set; }

        [JsonPropertyName("weightUnit")]
        public string WeightUnit { get; set; }
    }
}
