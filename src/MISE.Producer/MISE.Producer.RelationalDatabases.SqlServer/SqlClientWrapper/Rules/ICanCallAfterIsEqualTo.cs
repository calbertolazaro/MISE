using System.Collections.Generic;
using System.Data;

namespace MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.SqlClientWrapper.Rules
{
	public interface ICanCallAfterIsEqualTo
	{
		ICanCallAfterIsEqualTo IsEqualTo<T>(T value);
	    ICanCallAfterIn In<T>(IEnumerable<T> value);
	    ICanCallAfterAnd And(string columnName);
	    ICanCallAfterOrdering OrderBy(string columnName, OrderByCondition.SortMode orderMode = OrderByCondition.SortMode.Ascending);
		ICanCallAfterAdapt Adapt();
		string ToSql();
		void FillDataset(DataSet dataSet);
	}
}
