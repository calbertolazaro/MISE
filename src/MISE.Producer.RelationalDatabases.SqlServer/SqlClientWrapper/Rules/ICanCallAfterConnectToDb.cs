namespace MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.SqlClientWrapper.Rules
{
    public interface ICanCallAfterConnectToDb
    {
        ICanCallAfterUsingThisConnectionString UsingThisConnectionString(string connectionString);
    }
}
