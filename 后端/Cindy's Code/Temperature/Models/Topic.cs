using System;
using System.Collections.Generic;

namespace Temperature.Models
{
    public partial class Topic
    {
        public Topic()
        {
            TopicAnswerReply = new HashSet<TopicAnswerReply>();
            TopicRank = new HashSet<TopicRank>();
        }

        public int TopicId { get; set; }
        public string TopicContent { get; set; }
        public int? AnswerNum { get; set; }
        public int? UserId { get; set; }
        public DateTime? TopicUploadTime { get; set; }
        public int? ZoneId { get; set; }
        public string TopicTitle { get; set; }

        public virtual User User { get; set; }
        public virtual Zone Zone { get; set; }
        public virtual ICollection<TopicAnswerReply> TopicAnswerReply { get; set; }
        public virtual ICollection<TopicRank> TopicRank { get; set; }
    }
}
