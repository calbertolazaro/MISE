using Ardalis.GuardClauses;
using MISE.Producer.Core.Abstractions;
using MISE.Producer.Core.RelationalDatabases.Abstractions;
using MISE.Producer.Core.RelationalDatabases.Schema;

namespace MISE.Producer.Core.RelationalDatabases.TableModule
{
    class Server : IServer
    {
        private readonly IAppLogger<Server> _logger;

        public Server(IAppLogger<Server> logger)
        {
            Guard.Against.Null(logger, nameof(logger));
            _logger = logger;
        }
        public void FillBy(RelationalDatabaseDataSet dataSet, 
            IServerGateway gateway)
        {
            _logger.Trace(Strings.TableModuleCall);
            Guard.Against.Null(dataSet, nameof(dataSet));
            Guard.Against.Null(gateway, nameof(gateway));

            RelationalDatabaseDataSet results = gateway.FindServer();
            dataSet.Merge(results);
        }
    }
}
