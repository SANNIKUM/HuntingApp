using AFFHA.Domain.Pictures;
using AFFHA.Domain.Zones;
using AFFHA.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AFFHA.API.Infrastructure.ServiceExtension
{
    public static class AddRepositoriesExtension
    {
        public static void AddAFFHARepositories(this IServiceCollection services)
        {
            services.AddTransient<IZoneRepository, ZoneRepository>();
            services.AddTransient<IPictureRepository, PictureRepository>();
        }
    }
}
