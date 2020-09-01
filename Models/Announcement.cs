using System;
using System.Collections.Generic;

namespace Temperature.Models
{
    public partial class Announcement
    {
        public int AnnouncementId { get; set; }
        public string AnnouncementContent { get; set; }
        public DateTime? AnnouncementUploadTime { get; set; }
        public int? UserId { get; set; }

        public virtual User User { get; set; }
    }
}
