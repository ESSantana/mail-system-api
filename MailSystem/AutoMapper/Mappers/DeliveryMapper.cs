using AutoMapper;
using MailSystem.API.DTO;
using MailSystem.Core.Entities;
using MailSystem.Core.Entities.Models;

namespace MailSystem.API.AutoMapper.Mappers
{
    /// <summary>
    /// Class to handle Delivery mapping
    /// </summary>
    public static class DeliveryMapper
    {
        /// <summary>
        /// Method that contains mapper trackers
        /// </summary>
        /// <param name="profile">Object that creates mappers between objects</param>
        public static void Map(Profile profile)
        {
            profile.CreateMap<DeliveryDTO, Delivery>();
            profile.CreateMap<Delivery, DeliveryDTO>();
            profile.CreateMap<DeliveryModel, Delivery>();
            profile.CreateMap<Delivery, DeliveryModel>();
        }
    }
}
