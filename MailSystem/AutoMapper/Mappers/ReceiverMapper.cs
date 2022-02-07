using AutoMapper;
using MailSystem.API.DTO;
using MailSystem.Core.Entities;
using MailSystem.Core.Entities.Models;

namespace MailSystem.API.AutoMapper.Mappers
{
    public static class ReceiverMapper
    {
        public static void Map(Profile profile)
        {
            profile.CreateMap<ReceiverDTO, Receiver>();
            profile.CreateMap<Receiver, ReceiverDTO>();
            profile.CreateMap<ReceiverModel, Receiver>();
            profile.CreateMap<Receiver, ReceiverModel>();

            // Receiver Documents Mapping
            profile.CreateMap<DocumentDTO, Document>();
            profile.CreateMap<Document, DocumentModel>();
            profile.CreateMap<DocumentModel, Document>();
            profile.CreateMap<Document, DocumentDTO>();
        }
    }
}
