using System;
using Ardalis.GuardClauses;
using MISE.Producer.Core.Abstractions;
using MISE.Producer.Core.RelationalDatabases.Abstractions;
using MISE.Producer.Core.RelationalDatabases.Schema;

namespace MISE.Producer.Core.RelationalDatabases.TableModule
{
    /// <summary>
    /// Implementação do módulo de tabela "Catalog"
    /// </summary>
    internal class Catalog : ICatalog
    {
        private readonly IAppLogger<Catalog> _logger;
       
        public Catalog(IAppLogger<Catalog> logger)
        {
            Guard.Against.Null(logger, nameof(IAppLogger<Catalog>));
            _logger = logger;
        }
        public RelationalDatabaseDataSet GetAll(ICatalogGateway gateway)
        {
            _logger.Trace(Strings.TableModuleCall);
            Guard.Against.Null(gateway, nameof(gateway));

            return gateway.FindAllCatalogs();
        }

        public RelationalDatabaseDataSet GetBy(ICatalogGateway catalogGateway, CatalogName[] catalogNameCollection)
        {
            _logger.Trace(Strings.TableModuleCall);
            Guard.Against.Null(catalogGateway, nameof(catalogGateway));
            Guard.Against.NullOrEmptyCollection(catalogNameCollection, nameof(catalogNameCollection));

            var dataSet = new RelationalDatabaseDataSet();
            FillBy(dataSet, catalogGateway, catalogNameCollection);

            return dataSet;
        }

        public void FillBy(RelationalDatabaseDataSet dataSet, ICatalogGateway catalogGateway, CatalogName[] catalogNameCollection)
        {
            _logger.Trace(Strings.TableModuleCall);
            Guard.Against.Null(catalogGateway, nameof(catalogGateway));
            Guard.Against.NullOrEmptyCollection(catalogNameCollection, nameof(catalogNameCollection));

            RelationalDatabaseDataSet results = catalogGateway.FindCatalogBy(catalogNameCollection);
            dataSet.Merge(results);
        }
    }
}
