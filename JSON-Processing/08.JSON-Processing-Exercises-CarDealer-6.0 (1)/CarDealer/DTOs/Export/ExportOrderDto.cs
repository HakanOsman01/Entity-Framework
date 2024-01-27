using Newtonsoft.Json;

namespace CarDealer.DTOs.Export
{
    public class ExportOrderDto
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("BirthDate")]
        public DateTime BirthDate { get; set; }
        [JsonProperty("IsYoungDriver")]
        public bool IsYoungDriver { get;set; }
    }
}
