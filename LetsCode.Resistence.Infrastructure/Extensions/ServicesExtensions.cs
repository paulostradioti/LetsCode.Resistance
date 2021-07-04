using LetsCode.Resistance.Domain;
using LetsCode.Resistance.Infrastructure.Maps;
using LetsCode.Resistance.Infrastructure.Respositories;
using LetsCode.Resistance.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddAutoMapper(typeof(MapProfiles));
        }
    }
}