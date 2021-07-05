using LetsCode.Resistance.Domain;
using LetsCode.Resistance.Infrastructure.Map;
using LetsCode.Resistance.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using LetsCode.Resistance.Infrastructure.Repository.Base;
using LetsCode.Resistance.Infrastructure.Repository.Interface;
using LetsCode.Resistance.Infrastructure.Service;
using LetsCode.Resistance.Infrastructure.Service.Interface;

namespace LetsCode.Resistance.Infrastructure.Extension
{
    public static class ServicesExtensions
    {
        public static void AddDatabaseServices(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("ResistanceNetwork"));
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IRebelRepository, RebelRepository>();
            services.AddTransient<IRebelService, RebelService>();
            services.AddTransient<IPriceRepository, PriceRepository>();
            services.AddTransient<ITradeService, TradeService>();
            services.AddTransient<IReportService, ReportService>();

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
            await context.SaveChangesAsync();
        }
    }
}