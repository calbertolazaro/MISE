using MISE.Producer.Core.RelationalDatabases.Schema;

namespace MISE.Producer.Core.RelationalDatabases.Abstractions
{
    public interface IServerGateway
    {
        RelationalDatabaseDataSet FindServer();
    }
}