namespace AFFHA.Infrastructure.Extensions
{
    using System.Linq.Expressions;
    using AFFHA.Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    public static class SoftDeleteExtensions
    {
        public static void UseSoftDelete(this ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDeletable).IsAssignableFrom(entityType.ClrType) == true)
                {
                    var entityTypeBuilder = builder.Entity(entityType.ClrType);
                    entityTypeBuilder.AddIsDeletedProperty();
                    entityTypeBuilder.AddIsDeletedQueryFilter();
                }
            }
        }

        private static void AddIsDeletedProperty(this EntityTypeBuilder config)
        {
            config.Metadata.AddProperty("IsDeleted", typeof(bool));
        }

        private static void AddIsDeletedQueryFilter(this EntityTypeBuilder config)
        {
            var parameter = Expression.Parameter(config.Metadata.ClrType);
            var propertyMethod = typeof(EF).GetMethod("Property").MakeGenericMethod(typeof(bool));
            var isDeletedProperty = Expression.Call(propertyMethod, parameter, Expression.Constant("IsDeleted"));
            var compareExpression = Expression.MakeBinary(ExpressionType.Equal, isDeletedProperty, Expression.Constant(false));
            var lambda = Expression.Lambda(compareExpression, parameter);

            config.HasQueryFilter(lambda);
        }
    }
}
