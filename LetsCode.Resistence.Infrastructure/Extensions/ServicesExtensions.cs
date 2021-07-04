using LetsCode.Resistance.Domain;
using LetsCode.Resistance.Infrastructure.Maps;
using LetsCode.Resistance.Infrastructure.Respositories;
using LetsCode.Resistance.Infrastructure.Services;
using LetsCode.Resistance.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace LetsCode.Resistance.Infrastructure.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddDatabaseServices(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("ResistanceNetwork"));
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IRepository<Rebel>, Repository<Rebel>>();
            services.AddTransient<IRebelService, RebelService>();
            services.AddTransient<IRepository<Price>, Repository<Price>>();
            services.AddTransient<ITradeService, TradeService>();
            services.AddAutoMapper(typeof(MapProfiles));
        }

        public static async Task SeedDatabase(AppDbContext context)
        {
            var prices = new[]
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
            };

            await context.Prices.AddRangeAsync(prices);
            context.SaveChanges();
        }
    }
}