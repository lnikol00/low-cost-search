using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TravelApp.Models.Amadeus
{
    public class Price
    {
        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("total")]
        public string Total { get; set; }

        [JsonPropertyName("base")]
        public string Base { get; set; }

        [JsonPropertyName("fees")]
        public List<Fee> Fees { get; set; }

        [JsonPropertyName("grandTotal")]
        public string GrandTotal { get; set; }
    }
}
