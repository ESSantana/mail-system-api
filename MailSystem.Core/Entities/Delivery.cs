using System;

namespace MailSystem.Core.Entities
{
    public class Delivery
    {
        public long Id { get; set; }
        public long ReceiverId { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string TrackCode { get; set; }
        public string DeliveredTo { get; set; }
        public DateTime ArrivedAt { get; set; }
        public DateTime DeliveredAt { get; set; }
        public DateTime DeletedAt { get; set; }

        public virtual Receiver Receiver { get; set; }
    }
}
