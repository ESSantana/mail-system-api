using MailSystem.Core.Entities.Models;
using System.Collections.Generic;

namespace MailSystem.Test.Configuration.Repository
{
    public static class DeliveryMockResult
    {
        public static List<DeliveryModel> Get()
        {
            return new List<DeliveryModel>
            {
                new DeliveryModel
                {
                    Id = 1,
                    Description = "Example Mock Description 1"
                },
                new DeliveryModel
                {
                    Id = 2,
                    Description = "Example Mock Description 2"
                },
                new DeliveryModel
                {
                    Id = 3,
                    Description = "Example Mock Description 3"
                }
            };
        }
    }
}
