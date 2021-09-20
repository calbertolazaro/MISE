using System.Data;

namespace MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.SqlClientWrapper.Rules
{
	public interface ICanCallAfterAscOrDesc
	{
		ICanCallAfterOrdering ThenBy(string columnName, OrderByCondition.SortMode orderMode = OrderByCondition.SortMode.Ascending);
		ICanCallAfterAdapt Adapt();
		string ToSql();
		void FillDataset(DataSet dataSet);
	}
}
