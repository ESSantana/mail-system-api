using AutoMapper;
using MailSystem.API.DTO;
using MailSystem.Core.Entities;
using MailSystem.Core.Entities.Models;

namespace MailSystem.API.AutoMapper.Mappers
{
    /// <summary>
    /// Class to handle Address mapping
    /// </summary>
    public static class AddressMapper
    {
        /// <summary>
        /// Method that contains mapper trackers
        /// </summary>
        /// <param name="profile">Object that creates mappers between objects</param>
        public static void Map(Profile profile)
        {
            profile.CreateMap<AddressDTO, Address>();
            profile.CreateMap<Address, AddressDTO>();
            profile.CreateMap<AddressModel, Address>();
            profile.CreateMap<Address, AddressModel>();
        }
    }
}
