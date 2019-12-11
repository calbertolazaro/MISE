using MISE.Producer.Core.RelationalDatabases.Schema;
using MISE.Producer.Core.RelationalDatabases.TableModule;

namespace MISE.Producer.Core.RelationalDatabases.Abstractions
{
    internal interface IServer
    {
        void FillBy(RelationalDatabaseDataSet dataSet, IServerGateway gateway);
    }
}