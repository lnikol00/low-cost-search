using System.Text.Json.Serialization;

namespace TravelApp.Models.Amadeus
{
    public class Operating
    {
        [JsonPropertyName("carrierCode")]
        public string CarrierCode { get; set; }
    }
}
