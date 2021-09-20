using System.Data;

namespace MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.SqlClientWrapper.Rules
{
	public interface ICanCallAfterIsNotEqualTo
	{
		ICanCallAfterIsNotEqualTo IsNotEqualTo<T>(T value);
	    ICanCallAfterAnd And(string columnName);
        ICanCallAfterOrdering OrderBy(string columnName, OrderByCondition.SortMode orderMode = OrderByCondition.SortMode.Ascending);
		ICanCallAfterAscOrDesc Asc();
		ICanCallAfterAscOrDesc Desc();
	    ICanCallAfterOrdering ThenBy(string columnName, OrderByCondition.SortMode orderMode = OrderByCondition.SortMode.Ascending);
		string ToSql();
		void FillDataset(DataSet dataSet);
	}
}
