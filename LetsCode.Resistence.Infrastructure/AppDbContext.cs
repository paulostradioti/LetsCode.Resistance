using LetsCode.Resistance.Domain;
using LetsCode.Resistance.Infrastructure.Extension;
using LetsCode.Resistance.Infrastructure.Map;
using Microsoft.EntityFrameworkCore;

namespace LetsCode.Resistance.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.AddConfiguration(new RebelMap());
            builder.AddConfiguration(new PriceMap());
        }

        public DbSet<Rebel> Rebels { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
    }
}