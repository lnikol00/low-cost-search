using System;
using System.Text.Json.Serialization;

namespace TravelApp.Models.Amadeus
{
    public class Arrival
    {
        [JsonPropertyName("iataCode")]
        public string IataCode { get; set; }

        [JsonPropertyName("terminal")]
        public string Terminal { get; set; }

        [JsonPropertyName("at")]
        public DateTime At { get; set; }
    }
}
