namespace Afforestation.WebUI.Models.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class SiteEditViewModel
    {
        [Required(ErrorMessage = "Saha adı zorunludur")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(-90, 90, ErrorMessage = "Enlem -90 ile 90 arasında olmalıdır")]
        public double Latitude { get; set; }

        [Required]
        [Range(-180, 180, ErrorMessage = "Boylam -180 ile 180 arasında olmalıdır")]
        public double Longitude { get; set; }

        [Required]
        public string City { get; set; } = string.Empty;

        [Required]
        public string District { get; set; } = string.Empty;

        [Required]
        public string PlantingData { get; set; } = string.Empty;

        [Required]
        public string Status { get; set; } = string.Empty;
    }
}