﻿using Newtonsoft.Json;

namespace CarDealer.DTOs.Import
{
    public class CarDto
    {
        public string Model { get; set; }
        public string Make { get; set; }
        public long TraveledDistance { get; set; }

        public int[] PartsId { get; set; }
    }
}
