using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;
using MISE.Producer.Core.RelationalDatabases.Abstractions;
using MISE.Producer.Core.RelationalDatabases.Attributes;

namespace MISE.Producer.Core.RelationalDatabases.Infrastructure
{
    internal class RelationalDatabaseTableGatewayLoader : IRelationalDatabaseTableGatewayLoader
    {
        private readonly HashSet<Type> _hashTableGateway = new HashSet<Type>();

        public IEnumerable<Type> TableGateways => _hashTableGateway;

        public IEnumerable<Type> LoadFromRuntime()
        {
            //_logger.LogInformation("A reconhecer bibliotecas de infra-estrutura");
            // Assemblies que começam por "MISE.Producer.Infrastructure.RelationalDatabases"
            List<Assembly> assemblies = new List<Assembly>();

            var dependencies = 
                DependencyContext.Default.RuntimeLibraries
                    .Where(arg => arg.Name.StartsWith("MISE.Producer"));

            foreach (var lib in dependencies)
            {
                try
                {
                    var assembly = Assembly.Load(new AssemblyName(lib.Name));
                    assemblies.Add(assembly);
                }
                catch (FileNotFoundException) { }
            }

            var tableGateways = new List<Type>();

            FindImplementedInterface(assemblies, ref tableGateways);

            _hashTableGateway.UnionWith(tableGateways);

            return tableGateways;
        }

        private void FindImplementedInterface(List<Assembly> assemblies, ref List<Type> relationalDatabaseTableGatewayAssemblies)
        {
            foreach (var assembly in assemblies)
                FindImplementedInterface(assembly, ref relationalDatabaseTableGatewayAssemblies);
        }

        private void FindImplementedInterface(Assembly assembly, ref List<Type> gatewayCollection)
        {
            var theClassTypes =
                from t in assembly.GetTypes()
                where t.GetTypeInfo().GetCustomAttribute<RelationalDatabaseGatewayMetadataAttribute>() != null
                      && t.GetTypeInfo().ImplementedInterfaces.Contains(typeof(IRelationalDatabaseGateway))
                select t;

            gatewayCollection.AddRange(theClassTypes.ToList());
        }
    }
}
