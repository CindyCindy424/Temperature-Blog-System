using System;
using System.Collections.Generic;

namespace Temperature.Models
{
    public partial class FavouriteArticle
    {
        public int FavouriteId { get; set; }
        public int ArticleId { get; set; }
        public DateTime? FavouriteTime { get; set; }

        public virtual Article Article { get; set; }
        public virtual Favourite Favourite { get; set; }
    }
}
