using Infra.Persistance.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.ExceptionHandlers.Configurations
{
    public static class ExceptionHandlerConfigurations
    {
        public static IServiceCollection AddExceptionHandling(this IServiceCollection services)
        {
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();
            return services;
        }

        public static WebApplication UseExceptionHandling(this WebApplication app)
        {
            app.UseExceptionHandler();
            return app;
        }
    }
}
