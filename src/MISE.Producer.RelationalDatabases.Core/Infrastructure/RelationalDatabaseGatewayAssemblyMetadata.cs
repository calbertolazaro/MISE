using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Ardalis.GuardClauses;
using MISE.Producer.Core.Abstractions;
using MISE.Producer.Core.RelationalDatabases.Abstractions;
using MISE.Producer.Core.RelationalDatabases.Attributes;

namespace MISE.Producer.Core.RelationalDatabases.Infrastructure
{
    /// <summary>
    /// Implementação para fornecer gateways.
    /// </summary>
    internal class RelationalDatabaseGatewayAssemblyMetadata : IRelationalDatabaseGatewayAssemblyMetadata
    {
        private readonly IAppLogger<RelationalDatabaseGatewayAssemblyMetadata> _logger;
        private readonly IRelationalDatabaseTableGatewayLoader _loader;

        public RelationalDatabaseGatewayAssemblyMetadata(IAppLogger<RelationalDatabaseGatewayAssemblyMetadata> logger,
            IRelationalDatabaseTableGatewayLoader loader)
        {
            Guard.Against.Null(logger, nameof(logger));
            Guard.Against.Null(loader, nameof(loader));
            _logger = logger;
            _loader = loader;
        }

        public IEnumerable<BridgeMetadata> GetMetadata(Type[] assemblyTypeCollection)
        {
            foreach (var assemblyType in assemblyTypeCollection)
                yield return GetMetadata(assemblyType);
        }

        public IEnumerable<BridgeMetadata> GetRuntimeMetadata()
        {
            return GetMetadata(_loader.TableGateways.ToArray());
        }

        public BridgeMetadata GetMetadata(Type assemblyType)
        {
            var attribute = assemblyType.GetTypeInfo().GetCustomAttribute<RelationalDatabaseGatewayMetadataAttribute>();

            return
                new BridgeMetadata
                {
                    Category = BridgeElement.Category,
                    // Informação que vem no assembly
                    Title = attribute.Title,
                    Creator = attribute.Creator,
                    Description = attribute.Description,
                    Publisher = attribute.Publisher,
                    // Informação associada ao nome completo do tipo e a localização da assembly no sistema de ficheiros
                    TypeToCreate = assemblyType.AssemblyQualifiedName,
                    Location = assemblyType.Assembly.Location
                };
        }
    }
}
