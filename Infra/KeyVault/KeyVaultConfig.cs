using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Infra.KeyVault
{
    public static class KeyVaultConfig
    {
        public static IConfigurationBuilder AddAzureKeyVaultToConfig(this IConfigurationBuilder configurationBuilder, IConfiguration configuration)
        {
            configurationBuilder.AddAzureKeyVault(new Uri(configuration.GetValue<string>("KeyVault:VaultUrl")!),
                    new DefaultAzureCredential());

            return configurationBuilder;
        }
    }
}
