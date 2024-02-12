using System.Text.Json.Serialization;

namespace TravelApp.Models.Amadeus
{
    public class Aircraft
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("320")]
        public string _320 { get; set; }

        [JsonPropertyName("321")]
        public string _321 { get; set; }

        [JsonPropertyName("333")]
        public string _333 { get; set; }
    }
}
