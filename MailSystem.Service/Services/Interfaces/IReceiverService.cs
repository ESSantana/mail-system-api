using MailSystem.Core.Entities;
using System.Collections.Generic;

namespace MailSystem.Service.Services.Interfaces
{
    public interface IReceiverService
    {
        List<Receiver> Get(string name, string document);
        Receiver Get(long id);
        int Create(List<Receiver> receivers);
        Receiver Modify(Receiver receiver);
        int Delete(long id);
    }
}
