using System;
using System.Collections.Generic;

namespace MailSystem.Core.Entities.Models
{
    public class ReceiverModel
    {
        public long Id { get; set; }
        public long AddressId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }

        public virtual AddressModel Address { get; set; }
        public virtual List<DeliveryModel> Deliveries { get; set; }
        public virtual List<DocumentModel> Documents { get; set; }
    }
}
