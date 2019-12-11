using System;
using System.Collections.Generic;

namespace MISE.Producer.Core.RelationalDatabases.Abstractions
{
    /// <summary>
    /// Abstracção para obter metadata das pontes.
    /// </summary>
    public interface IRelationalDatabaseGatewayAssemblyMetadata
    {
        BridgeMetadata GetMetadata(Type assemblyType);
        IEnumerable<BridgeMetadata> GetMetadata(Type[] assemblyType);
        IEnumerable<BridgeMetadata> GetRuntimeMetadata();
    }
}