using MailSystem.Core.Resources;
using MailSystem.Repository.Context;
using MailSystem.Repository.Repositories;
using MailSystem.Repository.Repositories.Interfaces;
using MailSystem.Service.Services;
using MailSystem.Service.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MailSystem.API.Extensions
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
