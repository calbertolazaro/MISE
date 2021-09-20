using System.Collections.Generic;
using System.Data;

namespace MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.SqlClientWrapper
{
    /// <summary>
    /// Interface para a criação dos statements da cláusula WHERE
    /// </summary>
    public interface IWhereBuilder
    {
        string BuildWhere(IReadOnlyCollection<WhereCondition> whereConditions);
        void PrepareCommand(IDbCommand command);
    }
}