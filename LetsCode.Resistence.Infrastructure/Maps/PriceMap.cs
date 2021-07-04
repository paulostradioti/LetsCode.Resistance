using LetsCode.Resistance.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LetsCode.Resistance.Infrastructure.Maps
{
    public class PriceMap : EntityTypeConfiguration<Price>
    {
        public override void Map(EntityTypeBuilder<Price> builder)
        {
            builder.ToTable("Price");
            builder.HasKey(x => x.Id);
            builder.HasData(new Price[]
            {
                new Price
                {
                    Id = Guid.NewGuid(),
                    ItemName = "Arma",
                    PriceInPoints = 4
                },
                new Price
                {
                    Id = Guid.NewGuid(),
                    ItemName = "Munição",
                    PriceInPoints = 3
                },
                new Price
                {
                    Id = Guid.NewGuid(),
                    ItemName = "Água",
                    PriceInPoints = 2
                },
                new Price
                {
                    Id = Guid.NewGuid(),
                    ItemName = "Comida",
                    PriceInPoints = 1
                },
            });
        }
    }
}