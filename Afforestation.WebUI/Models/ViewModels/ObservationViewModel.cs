using System;

namespace Afforestation.WebUI.Models.ViewModels
{
    public class ObservationViewModel
    {
        public int Id { get; set; }
        public int SiteId { get; set; }
        public DateTime Date { get; set; }
        public int ProductivityScore { get; set; }
        public string Note { get; set; } = string.Empty;
    }
}