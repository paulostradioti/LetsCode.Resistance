using LetsCode.Resistance.Domain;
using LetsCode.Resistance.Infrastructure.Extensions;
using LetsCode.Resistance.Infrastructure.Maps;
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
        }

        public DbSet<Rebel> Rebels { get; set; }
    }
}