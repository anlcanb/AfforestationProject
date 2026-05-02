using System;
using System.Collections.Generic;
using System.Text;

namespace Afforestation.Core.DTO
{
    public class ObservationDto
    {
        public int Id { get; set; }
        public int SiteId { get; set; }
        public DateTime Date { get; set; }
        public int ProductivityScore { get; set; }
        public string? Note { get; set; }
    }
}
