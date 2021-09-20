namespace MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.SqlClientWrapper.Rules
{
    public interface ICanCallAfterUsingThisConnectionString
	{
	    ICanCallAfterForThisCatalog ForThisCatalog(string catalogName);
        ICanCallAfterForEachCatalog ForEachCatalog(string[] catalogNames);
        ICanCallAfterSelectAllColumns SelectAllColumns();
		ICanCallAfterSelectColumn SelectColumn(string columnName);
	}
}
