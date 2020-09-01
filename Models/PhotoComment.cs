using System;
using System.Collections.Generic;

namespace Temperature.Models
{
    public partial class PhotoComment
    {
        public int PhotoCommentId { get; set; }
        public string PhotoCommentContent { get; set; }
        public int? PhotoId { get; set; }
        public int? UserId { get; set; }
        public DateTime? PhotoCommentTime { get; set; }

        public virtual Photo Photo { get; set; }
        public virtual User User { get; set; }
    }
}
