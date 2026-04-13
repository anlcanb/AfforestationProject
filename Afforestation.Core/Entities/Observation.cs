using System;
using System.Collections.Generic;
using System.Text;

namespace Afforestation.Core.Entities
{
    public class Observation
    {
        public int Id { get; set; }
        public int SiteId { get; set; }
        public DateTime Date { get; set; }
        public int ProductivityScore { get; set; }
        public string? Note { get; set; }
        public Site? Site { get; set; }

    }
}
