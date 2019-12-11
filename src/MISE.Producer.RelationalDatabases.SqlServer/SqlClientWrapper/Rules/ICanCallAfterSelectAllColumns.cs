namespace MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.SqlClientWrapper.Rules
{
	public interface ICanCallAfterSelectAllColumns
	{
		ICanCallAfterFrom From(string tableName);
	}
}
