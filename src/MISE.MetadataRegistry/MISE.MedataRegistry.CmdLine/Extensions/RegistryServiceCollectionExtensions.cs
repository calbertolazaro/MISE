using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MISE.MetadataRegistry.Core;

namespace MISE.MetadataRegistry.CommandLine.Extensions
{
    internal static class RegistryServiceCollectionExtensions
    {
        internal static void AddRegistryServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            // Permite a utilização do serviço IOptions<RegistryOptions>
            serviceCollection.Configure<RegistryOptions>(o => configuration.GetSection("Registry").Bind(o));            
        }
    }
}