namespace Afforestation.WebUI.Models.ViewModels
{
    public class SiteEditViewModel
    {
        public string Name { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string City { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string PlantingData { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}