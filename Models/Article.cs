using System;
using System.Collections.Generic;

namespace Temperature.Models
{
    public partial class Article
    {
        public Article()
        {
            ArticleCommentReply = new HashSet<ArticleCommentReply>();
            ArticleRank = new HashSet<ArticleRank>();
            ArticleVisit = new HashSet<ArticleVisit>();
            FavouriteArticle = new HashSet<FavouriteArticle>();
        }

        public int ArticleId { get; set; }
        public string Title { get; set; }
        public int? UserId { get; set; }
        public string ArticleContent { get; set; }
        public int? ArticleLikes { get; set; }
        public int? CollectNum { get; set; }
        public int? ReadNum { get; set; }
        public DateTime? ArticleUploadTime { get; set; }
        public int? ZoneId { get; set; }

        public virtual User User { get; set; }
        public virtual Zone Zone { get; set; }
        public virtual ICollection<ArticleCommentReply> ArticleCommentReply { get; set; }
        public virtual ICollection<ArticleRank> ArticleRank { get; set; }
        public virtual ICollection<ArticleVisit> ArticleVisit { get; set; }
        public virtual ICollection<FavouriteArticle> FavouriteArticle { get; set; }
    }
}
