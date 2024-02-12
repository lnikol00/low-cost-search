using System.Text.Json.Serialization;

namespace TravelApp.Models.Amadeus
{
    public class FareDetailsBySegment
    {
        [JsonPropertyName("segmentId")]
        public string SegmentId { get; set; }

        [JsonPropertyName("cabin")]
        public string Cabin { get; set; }

        [JsonPropertyName("fareBasis")]
        public string FareBasis { get; set; }

        [JsonPropertyName("class")]
        public string Class { get; set; }

        [JsonPropertyName("includedCheckedBags")]
        public IncludedCheckedBags IncludedCheckedBags { get; set; }
    }
}
