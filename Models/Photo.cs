using System;
using System.Collections.Generic;

namespace Temperature.Models
{
    public partial class Photo
    {
        public Photo()
        {
            PhotoComment = new HashSet<PhotoComment>();
            PhotoVisit = new HashSet<PhotoVisit>();
        }

        public int PhotoId { get; set; }
        public int? AlbumId { get; set; }
        public int? PhotoLikes { get; set; }
        public int? VisitNum { get; set; }
        public string PhotoAddress { get; set; }
        public DateTime? PhotoUploadTime { get; set; }
        public int? UserId { get; set; }

        public virtual Album Album { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<PhotoComment> PhotoComment { get; set; }
        public virtual ICollection<PhotoVisit> PhotoVisit { get; set; }
    }
}
