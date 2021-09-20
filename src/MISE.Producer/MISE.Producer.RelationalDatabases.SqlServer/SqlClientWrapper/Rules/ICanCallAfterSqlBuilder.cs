namespace MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.SqlClientWrapper.Rules
{
    public interface ICanCallAfterSqlBuilder
    {
        ICanCallAfterSelectAllColumns SelectAllColumns();
        ICanCallAfterSelectColumn SelectColumn(string columnName);
    }
}