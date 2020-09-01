using System;
using System.Collections.Generic;

namespace Temperature.Models
{
    public partial class AlbumVisit
    {
        public int AlbumId { get; set; }
        public int UserId { get; set; }
        public DateTime? AlbumVisitTime { get; set; }

        public virtual Album Album { get; set; }
        public virtual User User { get; set; }
    }
}
