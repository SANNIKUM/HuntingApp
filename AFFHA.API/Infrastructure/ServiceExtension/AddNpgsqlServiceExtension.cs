using AFFHA.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System;
using Microsoft.Extensions.Configuration;

namespace AFFHA.API.Infrastructure.ServiceExtension
{
    public static class AddNpgsqlServiceExtension
    {
        public static IServiceCollection AddNpgsqlService(this IServiceCollection services, IConfiguration configuration)
        {
            var assemblyName = typeof(AFFHADbContext).GetTypeInfo().Assembly.GetName().Name;
            services
                .AddEntityFrameworkNpgsql()
                .AddDbContext<AFFHADbContext>(options =>
                {
                    options.UseNpgsql(configuration["ConnectionStrings:DefaultConnection"],
                        npgsqlOptionsAction: sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(assemblyName);
                            sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd:null);
                        }
                        );
                },
                ServiceLifetime.Scoped  //Showing explicitly that the DbContext is shared across the HTTP request scope (graph of objects started in the HTTP request)
            );

            return services;
        }
    }
}
