using System.Data.SqlClient;

namespace MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.Abstractions
{
    /// <summary>
    /// Contexto com dados de ligação ao fornecedor de dados.
    /// </summary>
    internal interface IGatewayContext
    {
        string ConnectionString { get; set; }
        SqlConnection Connection { get; }
    }
}