using System;
using System.Collections.Generic;

namespace Temperature.Models
{
    public partial class ArticleRank
    {
        public DateTime ArticleRankDate { get; set; }
        public TimeSpan ArticleRankTime { get; set; }
        public string ArticleRankType { get; set; }
        public string ArticleRank1 { get; set; }
        public int? ArticleId { get; set; }

        public virtual Article Article { get; set; }
    }
}
