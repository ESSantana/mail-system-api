using MailSystem.Core.Entities;
using System;
using System.Collections.Generic;

namespace MailSystem.Service.Services
{
    public interface IDeliveryService
    {
        List<Delivery> Get(string trackCode, string type, DateTime? arrivedDate);
        Delivery Get(long id);
        int Create(List<Delivery> deliveries);
        Delivery Modify(Delivery delivery);
        int Delete(long id);
    }
}
