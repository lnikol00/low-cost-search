using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TravelApp.Models.Amadeus
{
    public class Data
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("source")]
        public string Source { get; set; }

        [JsonPropertyName("instantTicketingRequired")]
        public bool InstantTicketingRequired { get; set; }

        [JsonPropertyName("nonHomogeneous")]
        public bool NonHomogeneous { get; set; }

        [JsonPropertyName("oneWay")]
        public bool OneWay { get; set; }

        [JsonPropertyName("lastTicketingDate")]
        public string LastTicketingDate { get; set; }

        [JsonPropertyName("numberOfBookableSeats")]
        public int NumberOfBookableSeats { get; set; }

        [JsonPropertyName("itineraries")]
        public List<Itinerary> Itineraries { get; set; }

        [JsonPropertyName("price")]
        public Price Price { get; set; }

        [JsonPropertyName("pricingOptions")]
        public PricingOptions PricingOptions { get; set; }

        [JsonPropertyName("validatingAirlineCodes")]
        public List<string> ValidatingAirlineCodes { get; set; }

        [JsonPropertyName("travelerPricings")]
        public List<TravelerPricing> TravelerPricings { get; set; }
    }
}
