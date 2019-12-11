using Ardalis.GuardClauses;
using MISE.Producer.Core.Abstractions;
using MISE.Producer.Core.RelationalDatabases.Abstractions;
using MISE.Producer.Core.RelationalDatabases.Infrastructure;
using MISE.Producer.Core.RelationalDatabases.Schema;
using System;
using System.Threading.Tasks;

namespace MISE.Producer.Core.RelationalDatabases.Application
{
    internal class RelationalDatabasesProducerService : IRelationalDatabasesProducerService, IRelationalDatabasesProducerServiceAsync
    {
        private readonly IAppLogger<RelationalDatabasesProducerService> _logger;
        private readonly IRelationalDatabaseTableModuleService _tableModule;
        private readonly Func<string, string, IRelationalDatabaseGateway> _tableGatewayFactoryFunc;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="tableModule"></param>
        /// <param name="tableGatewayFactoryFunc"></param>
        public RelationalDatabasesProducerService(
            IAppLogger<RelationalDatabasesProducerService> logger,
            IRelationalDatabaseTableModuleService tableModule,
            Func<string, string, IRelationalDatabaseGateway> tableGatewayFactoryFunc
            )
        {
            Guard.Against.Null(logger, nameof(logger));
            Guard.Against.Null(tableModule, nameof(tableModule));
            Guard.Against.Null(tableGatewayFactoryFunc, nameof(tableGatewayFactoryFunc));
            _logger = logger;
            _tableModule = tableModule;
            _tableGatewayFactoryFunc = tableGatewayFactoryFunc;
        }

        public RelationalDatabaseDataSet GetCatalogs(string dataSource, string connectionString)
        {
            Guard.Against.NullOrEmpty(dataSource, nameof(dataSource));
            Guard.Against.NullOrEmpty(connectionString, nameof(connectionString));
            try
            {
                ICatalogGateway gateway = _tableGatewayFactoryFunc(dataSource, connectionString);
                return _tableModule.Catalog.GetAll(gateway);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public RelationalDatabaseDataSet GetCatalogs(string dataSource, string connectionString, CatalogName[] catalogNameCollection)
        {
            Guard.Against.NullOrEmpty(dataSource, nameof(dataSource));
            Guard.Against.NullOrEmpty(connectionString, nameof(connectionString));
            Guard.Against.NullOrEmptyCollection(catalogNameCollection, nameof(catalogNameCollection));
            try
            {
                IRelationalDatabaseGateway gateway = _tableGatewayFactoryFunc(dataSource, connectionString);
                return _tableModule.Catalog.GetBy(gateway, catalogNameCollection);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public RelationalDatabaseDataSet GetTables(string dataSource, string connectionString, CatalogName[] catalogNameCollection)
        {
            Guard.Against.NullOrEmpty(dataSource, nameof(dataSource));
            Guard.Against.NullOrEmpty(connectionString, nameof(connectionString));
            Guard.Against.NullOrEmptyCollection(catalogNameCollection, nameof(catalogNameCollection));

            try
            {
                ITableGateway gateway = _tableGatewayFactoryFunc(dataSource, connectionString);
                return _tableModule.Table.GetBy(gateway, catalogNameCollection);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public RelationalDatabaseDataSet GetTables(string dataSource, string connectionString, CatalogName[] catalogNameCollection, TableName[] tableNameCollection)
        {
            Guard.Against.NullOrEmpty(dataSource, nameof(dataSource));
            Guard.Against.NullOrEmpty(connectionString, nameof(connectionString));
            Guard.Against.NullOrEmptyCollection(catalogNameCollection, nameof(catalogNameCollection));
            Guard.Against.NullOrEmptyCollection(tableNameCollection, nameof(tableNameCollection));

            try
            {
                ITableGateway gateway = _tableGatewayFactoryFunc(dataSource, connectionString);
                
                return _tableModule.Table.GetBy(gateway, catalogNameCollection, tableNameCollection);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public RelationalDatabaseDataSet GetSchema(string dataSource, string connectionString, CatalogName[] catalogNameCollection)
        {
            Guard.Against.NullOrEmpty(dataSource, nameof(dataSource));
            Guard.Against.NullOrEmpty(connectionString, nameof(connectionString));

            try
            {
                
                IRelationalDatabaseGateway gateway = _tableGatewayFactoryFunc(dataSource, connectionString);

                RelationalDatabaseDataSet dataSet = new RelationalDatabaseDataSet();
                // Activa mecanismo que suspende a validação das relações estrangeiras
                dataSet.EnforceConstraints = false;

                _tableModule.Server.FillBy(dataSet, gateway);
                _tableModule.Catalog.FillBy(dataSet, gateway, catalogNameCollection);
                _tableModule.Table.FillBy(dataSet, gateway, catalogNameCollection);
                _tableModule.Domain.FillBy(dataSet, gateway, catalogNameCollection);
                _tableModule.Column.FillBy(dataSet, gateway, catalogNameCollection);

                // Activa regras de relacionamento entre tabelas
                dataSet.EnforceConstraints = true;

                return dataSet;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        #region Async
        public async Task<RelationalDatabaseDataSet> GetCatalogsAsync(string dataSource, string connectionString)
        {
            Guard.Against.NullOrEmpty(dataSource, nameof(dataSource));
            Guard.Against.NullOrEmpty(connectionString, nameof(connectionString));
            try
            {
                // simula a execução assíncrona do componente de acesso aos dados
                //
                RelationalDatabaseDataSet dataSet = await Task.Run(async () =>
                {
                    await Task.Delay(1);
                    IRelationalDatabaseGateway gateway = _tableGatewayFactoryFunc(dataSource, connectionString);
                    return _tableModule.Catalog.GetAll(gateway);
                });

                return dataSet;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<RelationalDatabaseDataSet> GetSchemaAsync(string dataSource, string connectionString,
            CatalogName[] catalogNameCollection)
        {
            Guard.Against.NullOrEmpty(dataSource, nameof(dataSource));
            Guard.Against.NullOrEmpty(connectionString, nameof(connectionString));
            try
            {
                // simula a execução assíncrona do componente de acesso aos dados
                //
                RelationalDatabaseDataSet dataSet = await Task.Run(async () =>
                {
                    await Task.Delay(1000);
                    IRelationalDatabaseGateway gateway = _tableGatewayFactoryFunc(dataSource, connectionString);
                    
                    RelationalDatabaseDataSet ds = new RelationalDatabaseDataSet();
                    ds.EnforceConstraints = false;

                    _tableModule.Server.FillBy(ds, gateway);
                    _tableModule.Catalog.FillBy(ds, gateway, catalogNameCollection);
                    _tableModule.Table.FillBy(ds, gateway, catalogNameCollection);
                    _tableModule.Domain.FillBy(ds, gateway, catalogNameCollection);
                    _tableModule.Column.FillBy(ds, gateway, catalogNameCollection);

                    return ds;
                });

                return dataSet;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        #endregion
    }
}
