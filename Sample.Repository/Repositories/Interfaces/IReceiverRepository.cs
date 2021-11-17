using MailSystem.Core.Entities.Models;
using System;
using System.Collections.Generic;

namespace MailSystem.Repository.Repositories.Interfaces
{
    public interface IReceiverRepository
    {
        List<ReceiverModel> Get(string name, string document);
        ReceiverModel Get(long Id);
        int Create(List<ReceiverModel> entity);
        ReceiverModel Modify(ReceiverModel entity);
        int Delete(long Id);
    }
}
