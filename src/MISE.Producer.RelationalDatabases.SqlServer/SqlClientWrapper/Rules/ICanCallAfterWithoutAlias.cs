using System.Data;

namespace MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.SqlClientWrapper.Rules
{
    public interface ICanCallAfterWithoutAlias
    {
        ICanCallAfterWhere Where(string columnName);
        ICanCallAfterAdapt Adapt();
        string ToSql();
        void FillDataset(DataSet dataSet);
    }
}