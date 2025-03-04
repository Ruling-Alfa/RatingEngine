using Infra.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RatingEngine.Persistance.Interfaces;
using System.Threading.Tasks;

namespace RatingEngine.Persistance.Configurations
{
    public static class PersistanceConfigurations
    {
        public static IServiceCollection AddPersistanceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RatingEngineContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("RatingEngineContext"))
                .UseAsyncSeeding(async (context, _, ct) =>
                {
                    await new SeedData(context).InitSeedDataAsync();
                });
            });
            services.AddScoped<DbContext, RatingEngineContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddInfraLayer(configuration);
            return services;
        }
    }
}
