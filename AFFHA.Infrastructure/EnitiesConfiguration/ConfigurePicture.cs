namespace AFFHA.Infrastructure.EnitiesConfiguration
{
    using AFFHA.Domain;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class ConfigurePicture
    {
        internal static void Picture(EntityTypeBuilder<Picture> config)
        {
            config.HasOne(ss => ss.Zone)
                ;
                //.OnDelete(DeleteBehavior.Restrict);
        }
    }
}
