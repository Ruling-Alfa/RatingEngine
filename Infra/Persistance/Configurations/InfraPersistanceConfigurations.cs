using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infra.Persistance.Interfaces;

namespace Infra.Persistance.Configurations
{
    public static class InfraPersistanceConfigurations
    {
        public static IServiceCollection AddInfraPersistanceLayer(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            return services;
        }
    }
}
