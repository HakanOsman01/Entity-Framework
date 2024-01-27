using System.Text.Json.Serialization;

namespace CarDealer.DTOs.Export
{
    public class ExportToyotaCarsDto
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        [JsonPropertyName("Make")]
        public string Make { get; set; }
        [JsonPropertyName("Model")]
        public string Model { get; set; }
        [JsonPropertyName("TraveledDistance")]
        public long TraveledDistance { get; set; }


    }
}
