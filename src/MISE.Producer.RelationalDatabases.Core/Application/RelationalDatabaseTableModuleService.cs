using Ardalis.GuardClauses;
using MISE.Producer.Core.Abstractions;
using MISE.Producer.Core.RelationalDatabases.Abstractions;
using MISE.Producer.Core.RelationalDatabases.TableModule;

namespace MISE.Producer.Core.RelationalDatabases.Application
{
    internal class RelationalDatabaseTableModuleService : IRelationalDatabaseTableModuleService
    {
        private readonly IAppLogger<RelationalDatabaseTableModuleService> _logger;
        private readonly ICatalog _catalogModule;
        private readonly ITable _tableModule;
        private readonly IColumn _columnModule;
        private readonly IServer _serverModule;
        private readonly IDomain _domainModule;

        public RelationalDatabaseTableModuleService(IAppLogger<RelationalDatabaseTableModuleService> logger,
            ICatalog catalogModule,
            ITable tableModule,
            IColumn columnModule,
            IServer serverModule,
            IDomain domainModule)
        {
            Guard.Against.Null(logger, nameof(logger));
            Guard.Against.Null(catalogModule, nameof(catalogModule));
            Guard.Against.Null(tableModule, nameof(tableModule));
            Guard.Against.Null(columnModule, nameof(columnModule));
            Guard.Against.Null(serverModule, nameof(serverModule));
            Guard.Against.Null(domainModule, nameof(domainModule));
            _logger = logger;
            _catalogModule = catalogModule;
            _tableModule = tableModule;
            _columnModule = columnModule;
            _serverModule = serverModule;
            _domainModule = domainModule;
        }

        public ICatalog Catalog => _catalogModule;
        public ITable Table => _tableModule;
        public IColumn Column => _columnModule;
        public IServer Server => _serverModule;
        public IDomain Domain => _domainModule;
    }
}