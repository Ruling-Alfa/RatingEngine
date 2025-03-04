using Infra.ExceptionHandlers.Configurations;
using Infra.Persistance.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Configurations
{
    public static class InfraConfigurations
    {
        public static IServiceCollection AddInfraLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowAllCors",
                                  policy =>
                                  {
                                      policy.SetIsOriginAllowed(_ => true)
                                            .AllowAnyOrigin()
                                            .AllowAnyMethod()
                                            .AllowAnyHeader();
                                      //policy.AllowAnyOrigin()
                                      //      .AllowAnyMethod()
                                      //      .AllowAnyHeader();
                                  });
            });

            services.AddExceptionHandling();
            services.AddInfraPersistanceLayer();
            return services;
        }

        public static WebApplication UseInfraLayer(this WebApplication app)
        {
            app.UseCors("AllowAllCors");
            app.UseExceptionHandling();
            return app;
        }
    }
}
