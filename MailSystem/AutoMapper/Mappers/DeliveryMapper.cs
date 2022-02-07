using AutoMapper;
using MailSystem.API.DTO;
using MailSystem.Core.Entities;
using MailSystem.Core.Entities.Models;

namespace MailSystem.API.AutoMapper.Mappers
{
    public static class DeliveryMapper
    {
        public static void Map(Profile profile)
        {
            profile.CreateMap<DeliveryDTO, Delivery>();
            profile.CreateMap<Delivery, DeliveryDTO>();
            profile.CreateMap<DeliveryModel, Delivery>();
            profile.CreateMap<Delivery, DeliveryModel>();
        }
    }
}
