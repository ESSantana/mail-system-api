using MailSystem.Repository.Repositories;
using MailSystem.Repository.Repositories.Interfaces;
using MailSystem.Service.Services;
using MailSystem.Service.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Sample.Core.Resources;
using Sample.Repository.Context;
using Sample.Repository.Repositories;
using Sample.Repository.Repositories.Interfaces;
using Sample.Service.Services;

namespace Sample.API.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static void ConfigureServiceDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<MailSystemDbContext>();

            services.AddTransient<IResourceLocalizer, ResourceLocalizer>();
            services.AddTransient<IDeliveryService, DeliveryService>();
            services.AddTransient<IDeliveryRepository, DeliveryRepository>();
            services.AddTransient<IAddressService, AddressService>();
            services.AddTransient<IAddressRepository, AddressRepository>();
            services.AddTransient<IReceiverService, ReceiverService>();
            services.AddTransient<IReceiverRepository, ReceiverRepository>();
        }
    }
}
