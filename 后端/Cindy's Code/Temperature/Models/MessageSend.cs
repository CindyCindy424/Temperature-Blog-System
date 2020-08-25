using System;
using System.Collections.Generic;

namespace Temperature.Models
{
    public partial class MessageSend
    {
        public int MessageId { get; set; }
        public int UserId { get; set; }
        public byte? ReceiveState { get; set; }

        public virtual MessageLibrary Message { get; set; }
        public virtual User User { get; set; }
    }
}
