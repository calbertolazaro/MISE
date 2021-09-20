using Ardalis.GuardClauses;
using MISE.Producer.Core.RelationalDatabases.Abstractions;
using MISE.Producer.Core.RelationalDatabases.Schema;
using MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.Abstractions;

namespace MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.Gateways
{
    internal class ServerGateway : IServerGateway
    {
        private IGatewayContext _context;

        internal ServerGateway(IGatewayContext context)
        {
            Guard.Against.Null(context, nameof(context));
            _context = context;
        }

        public RelationalDatabaseDataSet FindServer()
        {
            var ds = new RelationalDatabaseDataSet();
            ds.Server.AddServerRow(_context.Connection.DataSource, _context.Connection.ServerVersion);
            return ds;
        }
    }
}