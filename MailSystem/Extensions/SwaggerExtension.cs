using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Reflection;

namespace MailSystem.API.Extensions
{
    public static class SwaggerExtension
    {
        public static void ConfigureServiceSwagger(this IServiceCollection services)
        {
            services
                .AddSwaggerGen()
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "MailSystem API",
                        Description = "An API to facilitate access mail deliveries",
                        TermsOfService = new Uri("https://example.com/terms"),
                        Contact = new OpenApiContact
                        {
                            Name = "Emerson Santana",
                            Email = "emerson29988@gmail.com",
                            Url = new Uri("https://github.com/ESSantana"),
                        },
                        License = new OpenApiLicense
                        {
                            Name = "Use under ExampleLicense",
                            Url = new Uri("https://example.com/license"),
                        }
                    });
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = "Example: 'Bearer abcdef123456'",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });

                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    c.IncludeXmlComments(xmlPath);
                });

        }

        public static void ConfigureAppSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger().UseSwaggerUI(c =>
            {
                c.DocExpansion(DocExpansion.None);
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MailSystem API");
            });
        }
    }
}
