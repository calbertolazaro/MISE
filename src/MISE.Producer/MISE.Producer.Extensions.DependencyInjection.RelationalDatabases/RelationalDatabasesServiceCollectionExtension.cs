using System;
using Microsoft.Extensions.DependencyInjection;
using MISE.Producer.Core.RelationalDatabases.Abstractions;
using MISE.Producer.Core.RelationalDatabases.Application;
using MISE.Producer.Core.RelationalDatabases.Infrastructure;
using MISE.Producer.Core.RelationalDatabases.TableModule;

namespace MISE.Producer.Extensions.DependencyInjection.RelationalDatabases
{
    public static class RelationalDatabasesServiceCollectionExtension
    {
        public static void AddProducerRelationalDatabases(this IServiceCollection serviceCollection)
        {
            // Fachada para o padrão Table Module. Apresenta ao cliente uma interface única para acesso à lógica de negócio.
            serviceCollection.AddTransient<IRelationalDatabasesProducerService, RelationalDatabasesProducerService>();
            serviceCollection.AddTransient<IRelationalDatabasesProducerServiceAsync, RelationalDatabasesProducerService>();

            // Lógica de negócio baseado no Padrão Table Module
            serviceCollection.AddTransient<IRelationalDatabaseTableModuleService, RelationalDatabaseTableModuleService>();
            serviceCollection.AddTransient<ICatalog, Catalog>();
            serviceCollection.AddTransient<ITable, Table>();
            serviceCollection.AddTransient<IColumn, Column>();
            serviceCollection.AddTransient<IServer, Server>();
            serviceCollection.AddTransient<IDomain, Domain>();

            // Loader das gateways
            RelationalDatabaseTableGatewayLoader loader = new RelationalDatabaseTableGatewayLoader();
            loader.LoadFromRuntime();
            foreach (Type implementationType in loader.TableGateways)
                serviceCollection.AddScoped(implementationType);

            serviceCollection.AddSingleton<IRelationalDatabaseTableGatewayLoader>(loader);

            // Metadata das gateways
            serviceCollection.AddTransient<IRelationalDatabaseGatewayAssemblyMetadata, RelationalDatabaseGatewayAssemblyMetadata>();
            
            // Criação das gateways com base no atributo de metadata "Title"
            serviceCollection.AddTransient<Func<string, string, IRelationalDatabaseGateway>>(serviceProvider => (title, connectionString) =>
            {
                var gam = serviceProvider.GetService<IRelationalDatabaseGatewayAssemblyMetadata>();

                foreach (var bridge in gam.GetRuntimeMetadata())
                {
                    if (0 == String.Compare(title, bridge.Title, StringComparison.OrdinalIgnoreCase))
                    {
                        IRelationalDatabaseGateway gateway = (IRelationalDatabaseGateway)serviceProvider.GetService(Type.GetType(bridge.TypeToCreate));
                        gateway.Configure(connectionString);
                        return gateway;
                    }
                }

                return null;
            });
        }
    }
}
