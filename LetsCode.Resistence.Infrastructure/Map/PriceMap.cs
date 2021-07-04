using LetsCode.Resistance.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LetsCode.Resistance.Infrastructure.Map
{
    public class PriceMap : EntityTypeConfiguration<Price>
    {
        public override void Map(EntityTypeBuilder<Price> builder)
        {
            builder.ToTable("Price");
            builder.HasKey(x => x.Id);
        }
    }
}