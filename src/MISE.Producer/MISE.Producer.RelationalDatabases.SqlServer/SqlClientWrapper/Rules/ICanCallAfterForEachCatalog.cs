namespace MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.SqlClientWrapper.Rules
{
    public interface ICanCallAfterForEachCatalog
    {
        ICanCallAfterSelectAllColumns SelectAllColumns();
        ICanCallAfterSelectColumn SelectColumn(string columnName);
    }
}