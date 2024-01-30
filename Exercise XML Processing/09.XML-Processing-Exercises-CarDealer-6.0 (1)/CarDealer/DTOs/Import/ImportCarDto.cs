using System.Xml.Serialization;

namespace CarDealer.DTOs.Import
{
    [XmlType("Car")]
    
    public class ImportCarDto
    {
        [XmlElement("make")]
        public string Make { get; set; }
        [XmlElement("model")]
        public int Model { get; set; }
        [XmlElement("traveledDistance")]
        public string TraveledDistance { get; set; }
        [XmlArray("parts")]
        public ImportPartsIdsDto[]  Parts { get; set; }

    }
}
