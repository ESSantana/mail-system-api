using AutoMapper;
using MailSystem.API.AutoMapper.Mappers;

namespace MailSystem.API.AutoMapper
{
    /// <summary>
    /// Class that register all mappers in the application
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AutoMapperProfile()
        {
            DeliveryMapper.Map(this);
            AddressMapper.Map(this);
            ReceiverMapper.Map(this);
        }
    }
}