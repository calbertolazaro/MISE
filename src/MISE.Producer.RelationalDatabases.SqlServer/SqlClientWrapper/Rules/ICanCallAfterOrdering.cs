using System.Data;

namespace MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.SqlClientWrapper.Rules
{
	public interface ICanCallAfterOrdering
    {
        ICanCallAfterOrdering ThenBy(string columnName, OrderByCondition.SortMode orderMode = OrderByCondition.SortMode.Ascending);
		ICanCallAfterAscOrDesc Asc();
		ICanCallAfterAscOrDesc Desc();
		ICanCallAfterAdapt Adapt();
		string ToSql();
		void FillDataset(DataSet dataSet);
	}
}
