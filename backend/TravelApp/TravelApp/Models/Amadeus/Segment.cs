using System.Text.Json.Serialization;

namespace TravelApp.Models.Amadeus
{
    public class Segment
    {
        [JsonPropertyName("departure")]
        public Departure Departure { get; set; }

        [JsonPropertyName("arrival")]
        public Arrival Arrival { get; set; }

        [JsonPropertyName("carrierCode")]
        public string CarrierCode { get; set; }

        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("aircraft")]
        public Aircraft Aircraft { get; set; }

        [JsonPropertyName("operating")]
        public Operating Operating { get; set; }

        [JsonPropertyName("duration")]
        public string Duration { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("numberOfStops")]
        public int NumberOfStops { get; set; }

        [JsonPropertyName("blacklistedInEU")]
        public bool BlacklistedInEU { get; set; }
    }
}
