using System;
using System.Collections.Generic;

namespace Temperature.Models
{
    public partial class PhotoVisit
    {
        public int PhotoId { get; set; }
        public int UserId { get; set; }
        public DateTime? PhotoVisitTime { get; set; }

        public virtual Photo Photo { get; set; }
        public virtual User User { get; set; }
    }
}
