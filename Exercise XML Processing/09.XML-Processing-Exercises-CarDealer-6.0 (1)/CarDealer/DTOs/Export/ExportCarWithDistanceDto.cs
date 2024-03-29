﻿using System.Xml.Serialization;

namespace CarDealer.DTOs.Export
{
    [XmlType("Car")]
    public class ExportCarWithDistanceDto
    {
        [XmlAttribute("make")]
        public string Make { get; set; }
        [XmlAttribute("Model")]
        public string Model { get; set; }

        [XmlAttribute("traveled-distance")]
        public long TraveledDistance { get; set; }
        [XmlArray("parts")]
        public ExportPartIdsDto[] ExportPartIdsDto { get; set; }


    }
}
