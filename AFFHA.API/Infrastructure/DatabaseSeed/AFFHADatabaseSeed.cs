using AFFHA.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AFFHA.API.Infrastructure.DatabaseSeed
{
    public class AFFHADatabaseSeed
    {
        public async Task SeedAsync(AFFHADbContext context, IOptions<DatabaseSeedSettings> settings, ILogger<AFFHADatabaseSeed> logger)
        {
            var useCustomizationData = settings.Value.UseCustomizationData;
            using (context)
            {
                try
                {
                    // Apply pending migrations if any
                    var Database = context.GetInfrastructure().GetService<IMigrator>();
                    Database.Migrate();
                    context.Database.EnsureCreated();
                    if (useCustomizationData)
                    {
                        //seed work will go here
                    }
                    await context.SaveChangesAsync();
                }
                catch(Exception ex)
                {
                    logger.LogWarning(ex, "Exception {ExceptionType} with message {Message} detected on creating or migrating database",  ex.GetType().Name, ex.Message);
                    throw ex;
                }
            }

        }


    }
}
