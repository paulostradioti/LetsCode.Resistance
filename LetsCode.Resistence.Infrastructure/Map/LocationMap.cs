using LetsCode.Resistance.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LetsCode.Resistance.Infrastructure.Map
{
    public class LocationMap : EntityTypeConfiguration<Location>
    {
        public override void Map(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("Location");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}