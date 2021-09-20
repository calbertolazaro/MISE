namespace MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.SqlClientWrapper.Rules
{
    public interface ICanCallAfterSelectColumn
    {
        ICanCallAfterSelectColumn SelectColumn(string columnName);
        ICanCallAfterFrom From(string tableName);
    }
}