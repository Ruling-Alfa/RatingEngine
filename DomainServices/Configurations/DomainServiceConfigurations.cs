using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RatingEngine.DomainServices.Interfaces;
using RatingEngine.DomainServices.MapToDto;
using RatingEngine.Persistance.Configurations;

namespace RatingEngine.DomainServices.Configurations
{
    public static class DomainServiceConfigurations
    {
        public static IServiceCollection AddDomainLayerLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureAutoMapperServices();
            services.AddScoped<IProvinceService, ProvinceService>();

            services.AddPersistanceLayer(configuration);
            return services;
        }
        public static WebApplication UseExceptionHandling(this WebApplication app)
        {
            app.UseExceptionHandling();
            return app;
        }
        private static IServiceCollection ConfigureAutoMapperServices(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingToDto());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }
    }
}
