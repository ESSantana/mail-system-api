using MailSystem.Core.Entities.Models;
using System;
using System.Collections.Generic;

namespace MailSystem.Repository.Repositories.Interfaces
{
    public interface IDeliveryRepository
    {
        List<DeliveryModel> Get(string trackCode, string type, DateTime? arrivedDate);
        DeliveryModel Get(long id);
        int Create(List<DeliveryModel> deliveries);
        DeliveryModel Modify(DeliveryModel deliveries);
        int Delete(long id);
    }
}
