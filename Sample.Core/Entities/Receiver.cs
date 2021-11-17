using System;
using System.Collections.Generic;

namespace MailSystem.Core.Entities
{
    public class Receiver
    {
        public long Id { get; set; }
        public long AddressId { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }

        public virtual Address Address { get; set; }
        public virtual List<Delivery> Deliveries { get; set; }
    }
}
