using System;

namespace MailSystem.API.DTO
{
    public class AddressDTO
    {
        public long Id { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Neighborhood { get; set; }
        public string ZipCode { get; set; }
    }
}
