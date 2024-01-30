using System.Xml.Serialization;

namespace CarDealer.DTOs.Import
{
    public class CarDto
    {
       
        public string Make { get; set; }
       
        public int Model { get; set; }
       
        public long TraveledDistance { get; set; }
        
    }
}
