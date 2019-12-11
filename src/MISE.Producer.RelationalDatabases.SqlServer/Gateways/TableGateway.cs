using System.Data.Common;
using Ardalis.GuardClauses;
using MISE.Producer.Core.RelationalDatabases.Abstractions;
using MISE.Producer.Core.RelationalDatabases.Schema;
using MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.Abstractions;
using MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.SqlClientWrapper;
using MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.SqlStatements;

namespace MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.Gateways
{
    internal class TableGateway : ITableGateway
    {
        private readonly IGatewayContext _connection;

        internal TableGateway(IGatewayContext connection)
        {
            Guard.Against.Null(connection, nameof(connection));
            _connection = connection;
        }

        public RelationalDatabaseDataSet FindTablesBy(CatalogName[] catalogNameCollection)
        {
            Guard.Against.NullOrEmptyCollection(catalogNameCollection, nameof(catalogNameCollection));

            RelationalDatabaseDataSet ds = new RelationalDatabaseDataSet();
            ds.EnforceConstraints = false;

            Dsl.ConnectToMsSqlServer()
                .UsingThisConnectionString(_connection.ConnectionString)
                    .ForEachCatalog(CatalogName.ToArray(catalogNameCollection))
                .SelectAllColumns()
                .From(TableStatements.From)
                .Adapt()
                    .UsingThisMapping(BuildTableMapping(ds))
                .FillDataset(ds)
                ;

            /*using (SqlDataAdapter adapter = new SqlDataAdapter(TableStatments.SELECT_ALL_TABLES, Connection))
            {
                adapter.TableMappings.Add(CreateTableMapping(RdmsDataSet.SchemaStructure.Table.TableName));

                foreach (string catalog in catalogs)
                {
                    Connection.ChangeDatabase(catalog);

                    adapter.Fill(ds);
                }
            }*/

            return ds;
        }

        public RelationalDatabaseDataSet FindTablesBy(CatalogName catalog, TableName[] tableNameCollection)
        {
            RelationalDatabaseDataSet ds = new RelationalDatabaseDataSet();
            ds.EnforceConstraints = false;
            
            Dsl.ConnectToMsSqlServer()
                .UsingThisConnectionString(_connection.ConnectionString)
                .ForThisCatalog(catalog.Name)
                    .SelectAllColumns()
                    .From(TableStatements.From)
                    .Where("TABLE_NAME").In(TableName.ToArray(tableNameCollection))
                .Adapt().UsingThisMapping(BuildTableMapping(ds))
                .FillDataset(ds);

            return ds;
        }

        private DataTableMapping BuildTableMapping(RelationalDatabaseDataSet ds)
        {
            var dtm = new DataTableMapping
            {
                SourceTable = "Table",
                DataSetTable = ds.Table.TableName
            };

            dtm.ColumnMappings.Add("TABLE_SERVER_NAME", ds.Table.ServerNameColumn.ColumnName);
            dtm.ColumnMappings.Add("TABLE_CATALOG_ID", ds.Table.CatalogIdentifierColumn.ColumnName);
            dtm.ColumnMappings.Add("TABLE_CATALOG", ds.Table.CatalogNameColumn.ColumnName);
            dtm.ColumnMappings.Add("TABLE_SCHEMA", ds.Table.SchemaNameColumn.ColumnName);
            dtm.ColumnMappings.Add("TABLE_NAME", ds.Table.TableNameColumn.ColumnName);
            dtm.ColumnMappings.Add("TABLE_CREATE_DATE", ds.Table.TableCreateDateColumn.ColumnName);
            dtm.ColumnMappings.Add("TABLE_MODIFY_DATE", ds.Table.TableModifyDateColumn.ColumnName);
            dtm.ColumnMappings.Add("TABLE_TYPE", ds.Table.TableTypeColumn.ColumnName);

            return dtm;
        }        
    }
}