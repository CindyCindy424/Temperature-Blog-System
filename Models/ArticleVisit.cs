using System;
using System.Collections.Generic;

namespace Temperature.Models
{
    public partial class ArticleVisit
    {
        public int UserId { get; set; }
        public int ArticleId { get; set; }
        public DateTime? ArticleVisitTime { get; set; }

        public virtual Article Article { get; set; }
        public virtual User User { get; set; }
    }
}
