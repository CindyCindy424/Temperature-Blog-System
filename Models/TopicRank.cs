using System;
using System.Collections.Generic;

namespace Temperature.Models
{
    public partial class TopicRank
    {
        public DateTime TopicRankDate { get; set; }
        public TimeSpan TopicRankTime { get; set; }
        public int? TopicId { get; set; }
        public string TopicRankType { get; set; }
        public string TopicRank1 { get; set; }

        public virtual Topic Topic { get; set; }
    }
}
