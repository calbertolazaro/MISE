using Ardalis.GuardClauses;
using MISE.Producer.Core.Abstractions;
using MISE.Producer.Core.RelationalDatabases.Abstractions;
using MISE.Producer.Core.RelationalDatabases.Schema;

namespace MISE.Producer.Core.RelationalDatabases.TableModule
{
    class Domain : IDomain
    {
        private readonly IAppLogger<Domain> _logger;

        public Domain(IAppLogger<Domain> logger)
        {
            Guard.Against.Null(logger, nameof(logger));
            _logger = logger;
        }

        public void FillBy(RelationalDatabaseDataSet dataSet, IDomainGateway domainGateway, CatalogName[] catalogNameCollection)
        {
            _logger.Trace(Strings.TableModuleCall);
            Guard.Against.Null(domainGateway, nameof(domainGateway));
            Guard.Against.NullOrEmptyCollection(catalogNameCollection, nameof(catalogNameCollection));
            domainGateway.FillDomainsBy(dataSet, catalogNameCollection);
        }
    }
}
