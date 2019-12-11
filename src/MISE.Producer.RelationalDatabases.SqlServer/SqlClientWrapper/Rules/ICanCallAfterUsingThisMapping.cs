using System.Data;

namespace MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.SqlClientWrapper.Rules
{
	public interface ICanCallAfterUsingThisMapping
	{
		string ToSql();
		void FillDataset(DataSet dataSet);
	}
}
