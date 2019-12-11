using System;
using System.Collections.Generic;

namespace MISE.Producer.Core.RelationalDatabases.Abstractions
{
    /// <summary>
    /// Abstracção de para reconhecimento das gateways em runtime
    /// </summary>
    internal interface IRelationalDatabaseTableGatewayLoader
    {
        IEnumerable<Type> LoadFromRuntime();
        IEnumerable<Type> TableGateways { get; }
    }
}