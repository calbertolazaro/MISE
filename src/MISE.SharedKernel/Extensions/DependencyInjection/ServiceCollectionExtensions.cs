using Microsoft.Extensions.DependencyInjection;
using MISE.SharedKernel.Abstractions;
using MISE.SharedKernel.Logging;

namespace MISE.SharedKernel.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void UseAppLogger(this IServiceCollection serviceCollection)
        {
            // Logging
            serviceCollection.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
        }
    }
}