using System;
using System.Collections.Generic;

namespace Temperature.Models
{
    public partial class UserFollow
    {
        public int ActiveUserId { get; set; }
        public int PassiveUserId { get; set; }

        public virtual User ActiveUser { get; set; }
        public virtual User PassiveUser { get; set; }
    }
}
