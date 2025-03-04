using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics;

namespace RatingEngine.AppHost.Configurations
{
    internal static class ResourceBuilderExtensions
    {

        internal static IResourceBuilder<T> WithScalar<T>(this IResourceBuilder<T> resourceBuilder) where T : IResourceWithEndpoints
        {
            return resourceBuilder.WithOpenApiDocs("scalar-docs", "Scalar API Documentation", "scalar");
        }
        private static IResourceBuilder<T> WithOpenApiDocs<T>(this IResourceBuilder<T> resourceBuilder,
            string name, string title, string urlPath) where T : IResourceWithEndpoints
        {
            return resourceBuilder.WithCommand(name, title, async _ =>
            {
                try
                {
                    await Task.CompletedTask;
                    var endpoint = resourceBuilder.GetEndpoint("https");
                    var url = $"{endpoint.Url}/{urlPath}";
                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                    return new ExecuteCommandResult() { Success = true };
                }
                catch (Exception ex)
                {
                    return new ExecuteCommandResult() { Success = false, ErrorMessage = ex.ToString() };
                }
            },
            ctx => ctx.ResourceSnapshot.HealthStatus == HealthStatus.Healthy ? ResourceCommandState.Enabled : ResourceCommandState.Disabled,
            iconName: "Document", iconVariant: IconVariant.Filled);
        }

    }
}
