using System;
using System.Collections.Generic;

namespace Afforestation.WebUI.Models.ViewModels
{
    public class SiteDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string City { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string PlantingData { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public IEnumerable<ObservationViewModel> Observations { get; set; } = Array.Empty<ObservationViewModel>();
    }
}