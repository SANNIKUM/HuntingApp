namespace AFFHA.Infrastructure.EnitiesConfiguration
{
    using AFFHA.Domain;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    internal class ConfigureZone
    {
        internal static void Zone(EntityTypeBuilder<Zone> config)
        {
            config
                .HasMany(z => z.Pictures);                
        }
    }
}
