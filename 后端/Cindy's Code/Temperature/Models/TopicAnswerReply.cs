using System;
using System.Collections.Generic;

namespace Temperature.Models
{
    public partial class TopicAnswerReply
    {
        public TopicAnswerReply()
        {
            InverseParentAnswer = new HashSet<TopicAnswerReply>();
        }

        public int TopicAnswerId { get; set; }
        public int? TopicId { get; set; }
        public int? AnswerLikes { get; set; }
        public string AnswerContent { get; set; }
        public int? UserId { get; set; }
        public DateTime? AnswerUploadTime { get; set; }
        public int? ParentAnswerId { get; set; }

        public virtual TopicAnswerReply ParentAnswer { get; set; }
        public virtual Topic Topic { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<TopicAnswerReply> InverseParentAnswer { get; set; }
    }
}
