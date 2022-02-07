using AutoMapper;
using MailSystem.API.DTO;
using MailSystem.Core.Entities;
using MailSystem.Core.Entities.Models;

namespace MailSystem.API.AutoMapper.Mappers
{
    public static class AddressMapper
    {
        public static void Map(Profile profile)
        {
            profile.CreateMap<AddressDTO, Address>();
            profile.CreateMap<Address, AddressDTO>();
            profile.CreateMap<AddressModel, Address>();
            profile.CreateMap<Address, AddressModel>();
        }
    }
}
