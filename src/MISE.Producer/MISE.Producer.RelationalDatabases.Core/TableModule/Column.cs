using Ardalis.GuardClauses;
using MISE.Producer.Core.Abstractions;
using MISE.Producer.Core.RelationalDatabases.Abstractions;
using MISE.Producer.Core.RelationalDatabases.Schema;

namespace MISE.Producer.Core.RelationalDatabases.TableModule
{
    /// <summary>
    /// Implementação do módulo-de-tabela "Column"
    /// </summary>
    internal class Column : IColumn
    {
        private readonly IAppLogger<Column> _logger;

        public Column(IAppLogger<Column> logger)
        {
            Guard.Against.Null(logger, nameof(IAppLogger<Column>));
            _logger = logger;
        }

        public void FillBy(RelationalDatabaseDataSet dataSet, IColumnGateway columnGateway, CatalogName[] catalogNameCollection)
        {
            _logger.Trace(Strings.TableModuleCall);
            Guard.Against.Null(columnGateway, nameof(columnGateway));
            Guard.Against.NullOrEmptyCollection(catalogNameCollection, nameof(catalogNameCollection));
            
            columnGateway.FillColumnsBy(dataSet,catalogNameCollection);
        }
    }

}
