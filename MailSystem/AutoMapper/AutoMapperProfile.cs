using AutoMapper;
using MailSystem.API.AutoMapper.Mappers;

namespace MailSystem.API.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            DeliveryMapper.Map(this);
            AddressMapper.Map(this);
            ReceiverMapper.Map(this);
        }
    }
}