namespace MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.SqlClientWrapper.Rules
{
	public interface ICanCallAfterForThisCatalog
	{
	    ICanCallAfterForThisCatalog ForThisCatalog(string catalogName);
	    ICanCallAfterSelectAllColumns SelectAllColumns();
	    ICanCallAfterSelectColumn SelectColumn(string columnName);
    }
}
