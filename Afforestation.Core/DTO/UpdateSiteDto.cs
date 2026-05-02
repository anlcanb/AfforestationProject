using System;
using System.Collections.Generic;
using System.Text;

namespace Afforestation.Core.DTO
{
    public class UpdateSiteDto
    {
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public DateTime PlantingData { get; set; }
        public string Status { get; set; }
    }
}
