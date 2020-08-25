using System;
using System.Collections.Generic;

namespace Temperature.Models
{
    public partial class MessageLibrary
    {
        public MessageLibrary()
        {
            MessageReceive = new HashSet<MessageReceive>();
            MessageSend = new HashSet<MessageSend>();
        }

        public int MessageId { get; set; }
        public string MessageType { get; set; }
        public string MessageContent { get; set; }
        public DateTime? MessageTime { get; set; }

        public virtual ICollection<MessageReceive> MessageReceive { get; set; }
        public virtual ICollection<MessageSend> MessageSend { get; set; }
    }
}
