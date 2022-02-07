using AutoMapper;
using MailSystem.API.AutoMapper;

namespace MailSystem.Test.Configuration.Factory
{
    public static class AutoMapperFactory
    {
        public static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            var mapper = new Mapper(config);

            return mapper;
        }
    }
}
