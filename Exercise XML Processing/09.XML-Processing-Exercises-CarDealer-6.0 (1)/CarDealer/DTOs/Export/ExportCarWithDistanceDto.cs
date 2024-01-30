using System.Xml.Serialization;

namespace CarDealer.DTOs.Export
{
    [XmlType("Car")]
    public class ExportCarWithDistanceDto
    {
        [XmlElement("make")]
        public string Make { get; set; }
        [XmlElement("Model")]
        public string Model { get; set; }

        [XmlElement("traveled-distance")]
        public long TraveledDistance { get; set; }

    }
}
