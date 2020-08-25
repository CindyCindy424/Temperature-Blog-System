using System;
using System.Collections.Generic;

namespace Temperature.Models
{
    public partial class ArticleCommentReply
    {
        public ArticleCommentReply()
        {
            InverseParentCr = new HashSet<ArticleCommentReply>();
        }

        public int ArticleCrId { get; set; }
        public string ArticleCrContent { get; set; }
        public int? ArticleId { get; set; }
        public int? UserId { get; set; }
        public DateTime? ArticleCrTime { get; set; }
        public int? ParentCrId { get; set; }

        public virtual Article Article { get; set; }
        public virtual ArticleCommentReply ParentCr { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<ArticleCommentReply> InverseParentCr { get; set; }
    }
}
