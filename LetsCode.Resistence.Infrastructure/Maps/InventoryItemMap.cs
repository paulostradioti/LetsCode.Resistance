using LetsCode.Resistance.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LetsCode.Resistance.Infrastructure.Maps
{
    public class InventoryItemMap : EntityTypeConfiguration<InventoryItem>
    {
        public override void Map(EntityTypeBuilder<InventoryItem> builder)
        {
            builder.ToTable("InventoryItem");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}