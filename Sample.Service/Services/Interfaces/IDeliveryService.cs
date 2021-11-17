using MailSystem.Core.Entities;
using System;
using System.Collections.Generic;

namespace Sample.Service.Services
{
    public interface IDeliveryService
    {
        List<Delivery> Get(string trackCode, string type, DateTime? arrivedDate);
        Delivery Get(long Id);
        int Create(List<Delivery> entities);
        Delivery Modify(Delivery entity);
        int Delete(long Id);
    }
}
