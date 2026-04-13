using System;

namespace Afforestation.WebUI.Models.ViewModels
{
    public class SiteMapDataViewModel
    {
        public int SiteId { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string City { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public int? ProductivityScore { get; set; }
        public string? Note { get; set; }
        public DateTime? ObservationDate { get; set; }
    }
}