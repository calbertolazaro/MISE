using System;
using System.Data;
using System.Data.SqlClient;
using Ardalis.GuardClauses;
using MISE.Producer.Core.Abstractions;
using MISE.Producer.Core.RelationalDatabases.Abstractions;
using MISE.Producer.Core.RelationalDatabases.Attributes;
using MISE.Producer.Core.RelationalDatabases.Schema;
using MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.Abstractions;
using MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.Gateways;
using MISE.Producer.RelationalDatabases.SqlServer;

namespace MISE.Producer.Infrastructure.RelationalDatabases.SqlServer
{
    [RelationalDatabaseGatewayMetadata(Title = "SqlServer", Description = "Gateway de acesso aos metadados do SQL Server", Creator= "Carlos Lázaro", Publisher = "MISE")]
    public class SqlServerGateway : IRelationalDatabaseGateway, IGatewayContext, IDisposable
    {
        private readonly IAppLogger<SqlServerGateway> _logger;

        private readonly CatalogGateway _catalogGateway;
        private readonly TableGateway _tableGateway; 
        private readonly ColumnGateway _columnGateway;
        private readonly ServerGateway _serverGateway;
        private readonly DomainGateway _domainGateway;
        private SqlConnection _connection;
        

        public SqlServerGateway(IAppLogger<SqlServerGateway> logger)
        {
            Guard.Against.Null(logger, nameof(logger));
            _logger = logger;
            _catalogGateway = new CatalogGateway(this);
            _tableGateway = new TableGateway(this);
            _columnGateway = new ColumnGateway(this);
            _serverGateway = new ServerGateway(this);
            _domainGateway = new DomainGateway(this);
        }

        public void Configure(string connectionString)
        {
            _logger.Trace(Strings.TableGatewayCall, args:connectionString);
            Guard.Against.NullOrEmpty(connectionString, nameof(connectionString));
            ConnectionString = connectionString;
        }

        #region IGatewayConnection

        public string ConnectionString { get; set; }

        public SqlConnection Connection
        {
            get
            {
                if (_connection == null)
                    _connection = new SqlConnection(ConnectionString);

                // Define ou actualiza a cadeia de ligação. Fecha a ligação caso seja redefinida.
                if (0 != String.Compare(_connection.ConnectionString, ConnectionString, StringComparison.OrdinalIgnoreCase))
                {
                    if (_connection.State == ConnectionState.Open)
                        _connection.Close();

                    _connection.ConnectionString = ConnectionString;
                }

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                return _connection;
            }
        }

        #endregion

        RelationalDatabaseDataSet ICatalogGateway.FindAllCatalogs()
        {
            _logger.Trace(Strings.TableGatewayCall);
            return _catalogGateway.FindAllCatalogs();
        }

        RelationalDatabaseDataSet ICatalogGateway.FindCatalogBy(CatalogName[] catalogNameCollection)
        {
            _logger.Trace(Strings.TableGatewayCall);
            return _catalogGateway.FindCatalogBy(catalogNameCollection);
        }
        
        RelationalDatabaseDataSet ITableGateway.FindTablesBy(CatalogName[] catalogNameCollection)
        {
            _logger.Trace(Strings.TableGatewayCall);
            return _tableGateway.FindTablesBy(catalogNameCollection);
        }

        RelationalDatabaseDataSet ITableGateway.FindTablesBy(CatalogName catalog, TableName[] tableNameCollection)
        {
            _logger.Trace(Strings.TableGatewayCall);
            return _tableGateway.FindTablesBy(catalog, tableNameCollection);
        }

        RelationalDatabaseDataSet IColumnGateway.FindColumnsBy(CatalogName[] catalogNameCollection)
        {
            _logger.Trace(Strings.TableGatewayCall);
            return _columnGateway.FindColumnsBy(catalogNameCollection);
        }

        void IColumnGateway.FillColumnsBy(RelationalDatabaseDataSet dataSet, CatalogName[] catalogNameCollection)
        {
            _logger.Trace(Strings.TableGatewayCall);
            _columnGateway.FillColumnsBy(dataSet, catalogNameCollection);
        }
        
        RelationalDatabaseDataSet IServerGateway.FindServer()
        {
            _logger.Trace(Strings.TableGatewayCall);
            return _serverGateway.FindServer();
        }

        void IDomainGateway.FillDomainsBy(RelationalDatabaseDataSet dataSet, CatalogName[] catalogNameCollection)
        {
            _logger.Trace(Strings.TableGatewayCall);
            _domainGateway.FillDomainsBy(dataSet, catalogNameCollection);
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }

    }
}
