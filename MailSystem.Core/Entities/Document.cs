using System;

namespace MailSystem.Core.Entities
{
    public class Document
    {
        public long Id { get; set; }
        public long ReceiverId { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }

        public virtual Receiver Receiver { get; set; }
    }
}
