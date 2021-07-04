using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LetsCode.Resistance.Infrastructure
{
    public abstract class EntityTypeConfiguration<TEntity> where TEntity : class
    {
        public abstract void Map(EntityTypeBuilder<TEntity> builder);
    }
}