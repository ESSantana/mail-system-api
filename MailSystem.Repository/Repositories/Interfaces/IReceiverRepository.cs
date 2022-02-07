using MailSystem.Core.Entities.Models;
using System.Collections.Generic;

namespace MailSystem.Repository.Repositories.Interfaces
{
    public interface IReceiverRepository
    {
        List<ReceiverModel> Get(string name, string document);
        ReceiverModel Get(long id);
        int Create(List<ReceiverModel> receivers);
        ReceiverModel Modify(ReceiverModel receiver);
        int Delete(long id);
    }
}
