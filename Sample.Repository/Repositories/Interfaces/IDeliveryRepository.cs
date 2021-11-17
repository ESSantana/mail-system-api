using MailSystem.Core.Entities.Models;
using System;
using System.Collections.Generic;

namespace MailSystem.Repository.Repositories.Interfaces
{
    public interface IDeliveryRepository
    {
        List<DeliveryModel> Get(string trackCode, string type, DateTime? arrivedDate);
        DeliveryModel Get(long Id);
        int Create(List<DeliveryModel> entity);
        DeliveryModel Modify(DeliveryModel entity);
        int Delete(long Id);
    }
}
