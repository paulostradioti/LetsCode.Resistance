using LetsCode.Resistance.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LetsCode.Resistance.Infrastructure.Maps
{
    public class RebelMap : EntityTypeConfiguration<Rebel>
    {
        public override void Map(EntityTypeBuilder<Rebel> builder)
        {
            builder.ToTable("Rebels");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasOne(x => x.Location);
        }
    }
}