using MailSystem.Core.Entities;
using System.Collections.Generic;

namespace MailSystem.Service.Services.Interfaces
{
    public interface IAddressService
    {
        List<Address> Get(string street, string number, string neighborhood, string zipcode);
        Address Get(long id);
        int Create(List<Address> addresses);
        Address Modify(Address address);
        int Delete(long id);
    }
}
