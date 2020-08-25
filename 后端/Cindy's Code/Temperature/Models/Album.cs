using System;
using System.Collections.Generic;

namespace Temperature.Models
{
    public partial class Album
    {
        public Album()
        {
            AlbumVisit = new HashSet<AlbumVisit>();
            Photo = new HashSet<Photo>();
        }

        public int AlbumId { get; set; }
        public string AlbumIntroduction { get; set; }
        public string AlbumName { get; set; }
        public DateTime? AlbumTime { get; set; }
        public int? UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<AlbumVisit> AlbumVisit { get; set; }
        public virtual ICollection<Photo> Photo { get; set; }
    }
}
