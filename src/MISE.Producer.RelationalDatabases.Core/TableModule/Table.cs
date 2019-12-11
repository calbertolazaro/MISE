using Ardalis.GuardClauses;
using MISE.Producer.Core.Abstractions;
using MISE.Producer.Core.RelationalDatabases.Abstractions;
using MISE.Producer.Core.RelationalDatabases.Schema;

namespace MISE.Producer.Core.RelationalDatabases.TableModule
{
    /// <summary>
    /// Implementação do módulo de tabela "Table"
    /// </summary>
    internal class Table : ITable
    {
        private readonly IAppLogger<Table> _logger;

        public Table(IAppLogger<Table> logger)
        {
            Guard.Against.Null(logger, nameof(IAppLogger<Table>));
            _logger = logger;
        }
        public RelationalDatabaseDataSet GetBy(ITableGateway tableGateway, CatalogName[] catalogNameCollection)
        {
            _logger.Trace(Strings.TableModuleCall);
            Guard.Against.Null(tableGateway, nameof(tableGateway));
            Guard.Against.NullOrEmptyCollection(catalogNameCollection, nameof(catalogNameCollection));

            return tableGateway.FindTablesBy(catalogNameCollection);
        }

        public RelationalDatabaseDataSet GetBy(ITableGateway tableGateway, CatalogName[] catalogNameCollection, TableName[] tableNameCollection)
        {
            _logger.Trace(Strings.TableModuleCall);
            Guard.Against.Null(tableGateway, nameof(tableGateway));
            Guard.Against.NullOrEmptyCollection(catalogNameCollection, nameof(catalogNameCollection));

            RelationalDatabaseDataSet results = tableGateway.FindTablesBy(catalogNameCollection[0], tableNameCollection);

            for (int i = 1; i < catalogNameCollection.Length; i++)
                results.Merge(tableGateway.FindTablesBy(catalogNameCollection[i], tableNameCollection));

            return results;
        }

        public void FillBy(RelationalDatabaseDataSet dataSet, ITableGateway tableGateway, CatalogName[] catalogNameCollection)
        {
            _logger.Trace(Strings.TableModuleCall);
            Guard.Against.Null(dataSet, nameof(dataSet));
            Guard.Against.Null(tableGateway, nameof(tableGateway));
            Guard.Against.NullOrEmptyCollection(catalogNameCollection, nameof(catalogNameCollection));

            RelationalDatabaseDataSet results = tableGateway.FindTablesBy(catalogNameCollection);
            dataSet.Merge(results);
        }
    }

}
