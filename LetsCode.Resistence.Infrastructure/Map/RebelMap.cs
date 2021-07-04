using LetsCode.Resistance.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LetsCode.Resistance.Infrastructure.Map
{
    public class RebelMap : EntityTypeConfiguration<Rebel>
    {
        public override void Map(EntityTypeBuilder<Rebel> builder)
        {
            builder.ToTable("Rebel");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasOne(x => x.Location);
            builder.HasMany(x => x.Inventory).WithOne();
        }
    }
}