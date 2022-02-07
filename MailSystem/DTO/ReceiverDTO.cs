using System.Collections.Generic;

namespace MailSystem.API.DTO
{
    public class ReceiverDTO
    {
        public long Id { get; set; }
        public long AddressId { get; set; }
        public string Name { get; set; }

        public virtual AddressDTO Address { get; set; }
        public virtual List<DeliveryDTO> Deliveries { get; set; }
        public virtual List<DocumentDTO> Documents { get; set; }
    }
}
