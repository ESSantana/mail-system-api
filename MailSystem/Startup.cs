using FluentValidation.AspNetCore;
using MailSystem.API.AutoMapper;
using MailSystem.API.Extensions;
using MailSystem.API.Filters;
using MailSystem.Core.Resources;
using MailSystem.Repository.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace MailSystem
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssembly(Assembly.Load("MailSystem.API")))
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                    {
                        var assemblyName = new AssemblyName(typeof(Resource).GetTypeInfo().Assembly.FullName);
                        return factory.Create("Resource", assemblyName.Name);
                    };
                })
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc(config =>
            {
                config.Filters.Add(typeof(ExceptionFilter));
            });

            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("pt-BR"),
                    };

                    options.DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR");
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;

                    options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
                }
            );

            services.AddControllers();
            services.AddCors();

            services.ConfigureServiceAuthentication();
            services.ConfigureServiceHealthCheck();
            services.ConfigureServiceDependencyInjection();
            services.ConfigureServiceSwagger();
            services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });

            services.AddDbContext<MailSystemDbContext>(opt => opt.UseMySql(Configuration.GetConnectionString("DevConnection"), new MySqlServerVersion("8.0.27")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var supportedCultures = new[] { new CultureInfo("pt-BR") };

            var localization = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(localization.Value);

            //app.UseHttpsRedirection();
            app.UseRouting();

            var useCache = Configuration.GetValue<bool>("GlobalOptions:UseCache");
            if (useCache)
            {
                app.ConfigureCacheResponseMiddleware();
            }

            app.ConfigureAppAuthentication();
            app.ConfigureAppHealthCheck();
            app.ConfigureAppSwagger();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
