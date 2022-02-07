using System;

namespace MailSystem.Core.Entities.Models
{
    public class AddressModel
    {
        public long Id { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Neighborhood { get; set; }
        public string ZipCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }

        public virtual ReceiverModel Receiver { get; set; }
    }
}
