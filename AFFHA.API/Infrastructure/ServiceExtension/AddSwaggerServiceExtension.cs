using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using System.Reflection;
using Microsoft.OpenApi.Models;

namespace AFFHA.API.Infrastructure.ServiceExtension
{
    public static class AddSwaggerServiceExtension
    {
        public static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {
            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "AFF Hunting App API v1",
                    Description = "AFF Hunting App ASP.NET Core Web API v1",
                    Contact = new OpenApiContact
                    {
                        Name = "AFF Hunting App",
                        Email = "",
                    },
                    License = new OpenApiLicense
                    {
                        Name = "License Name",
                    },
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(System.AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                    c.IncludeXmlComments(xmlPath);
            });

            return services;
        }
    }
}
