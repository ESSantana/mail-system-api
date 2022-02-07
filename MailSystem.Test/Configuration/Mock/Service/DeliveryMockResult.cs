using MailSystem.Core.Entities;
using System.Collections.Generic;

namespace Sample.Test.Configuration.Service
{
    public static class DeliveryMockResult
    {
        public static List<Delivery> Get()
        {
            return new List<Delivery>
            {
                new Delivery
                {
                    Id = 1,
                    Description = "Example Mock Description 1"
                },
                new Delivery
                {
                    Id = 2,
                    Description = "Example Mock Description 2"
                },
                new Delivery
                {
                    Id = 3,
                    Description = "Example Mock Description 3"
                }
            };
        }
    }
}
