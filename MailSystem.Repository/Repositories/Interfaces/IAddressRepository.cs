using MailSystem.Core.Entities.Models;
using System.Collections.Generic;

namespace MailSystem.Repository.Repositories.Interfaces
{
    public interface IAddressRepository
    {
        List<AddressModel> Get(string street, string number, string neighborhood, string zipcode);
        AddressModel Get(long id);
        int Create(List<AddressModel> addresses);
        AddressModel Modify(AddressModel address);
        int Delete(long id);
    }
}
