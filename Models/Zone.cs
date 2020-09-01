using System;
using System.Collections.Generic;

namespace Temperature.Models
{
    public partial class Zone
    {
        public Zone()
        {
            Article = new HashSet<Article>();
            Topic = new HashSet<Topic>();
        }

        public int ZoneId { get; set; }
        public string ZoneName { get; set; }
        public int? ZoneArticleNum { get; set; }
        public int? ZoneTopicNum { get; set; }

        public virtual ICollection<Article> Article { get; set; }
        public virtual ICollection<Topic> Topic { get; set; }
    }
}
