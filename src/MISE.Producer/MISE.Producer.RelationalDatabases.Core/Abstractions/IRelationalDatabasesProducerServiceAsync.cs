using System.Threading.Tasks;
using MISE.Producer.Core.RelationalDatabases.Schema;

namespace MISE.Producer.Core.RelationalDatabases.Abstractions
{
    /// <summary>
    /// Abstracção para interacção com o back-end do sistema de forma assíncrona
    /// </summary>

    public interface IRelationalDatabasesProducerServiceAsync
    {
        Task<RelationalDatabaseDataSet> GetCatalogsAsync(string dataSource, string connectionString);
        Task<RelationalDatabaseDataSet> GetSchemaAsync(string dataSource, string connectionString, CatalogName[] catalogNameCollection);
    }
}