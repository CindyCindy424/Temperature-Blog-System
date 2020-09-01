using System;
using System.Collections.Generic;

namespace Temperature.Models
{
    public partial class Favourite
    {
        public Favourite()
        {
            FavouriteArticle = new HashSet<FavouriteArticle>();
        }

        public int FavouriteId { get; set; }
        public string FavouriteName { get; set; }
        public int? ArticleNum { get; set; }
        public int? UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<FavouriteArticle> FavouriteArticle { get; set; }
    }
}
