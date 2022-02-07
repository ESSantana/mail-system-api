using MailSystem.API.Middleware;
using Microsoft.AspNetCore.Builder;

namespace MailSystem.API.Extensions
{
    public static class CacheResponseExtension
    {
        public static IApplicationBuilder ConfigureCacheResponseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CacheResponseMiddleware>();
        }
    }
}
