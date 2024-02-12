using System.Text.Json.Serialization;

namespace TravelApp.Models.Amadeus
{
    public class Locations
    {
        [JsonPropertyName("BKK")]
        public BKK BKK { get; set; }

        [JsonPropertyName("MNL")]
        public MNL MNL { get; set; }

        [JsonPropertyName("SYD")]
        public SYD SYD { get; set; }
    }
}
