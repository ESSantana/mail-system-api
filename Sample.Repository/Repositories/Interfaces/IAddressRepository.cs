using MailSystem.Core.Entities.Models;
using System.Collections.Generic;

namespace Sample.Repository.Repositories.Interfaces
{
    public interface IAddressRepository
    {
        List<AddressModel> Get(string street, string number, string neighborhood, string zipcode);
        AddressModel Get(long Id);
        int Create(List<AddressModel> entity);
        AddressModel Modify(AddressModel entity);
        int Delete(long Id);
    }
}
