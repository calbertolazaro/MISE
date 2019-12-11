using System.Data.Common;
using System.Data.SqlClient;
using Ardalis.GuardClauses;
using MISE.Producer.Core.RelationalDatabases.Abstractions;
using MISE.Producer.Core.RelationalDatabases.Schema;
using MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.Abstractions;
using MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.SqlStatements;

namespace MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.Gateways
{
    internal class CatalogGateway : ICatalogGateway
    {
        readonly IGatewayContext _context;
        
        internal CatalogGateway(IGatewayContext context)
        {
            Guard.Against.Null(context, nameof(context));
            _context = context;
        }
        
        public RelationalDatabaseDataSet FindAllCatalogs()
        {
            var ds = new RelationalDatabaseDataSet();
            //// 1. Open connection
            //using (SqlConnection connection = new SqlConnection(_context.ConnectionString))
            //{
            //    connection.Open();

            //    // 2. Create new adapter
            //    using (SqlDataAdapter adapter =
            //        new SqlDataAdapter(CatalogStatements.SELECT_ALL_CATALOGS, connection))
            //    {
            //        adapter.TableMappings.Add(BuildTableMapping());

            //        // 3. Use DataAdapter to fill typed datatable
            //        adapter.Fill(ds);
            //    }
            //}
            using (var adapter = new SqlDataAdapter(CatalogStatements.SELECT_ALL_CATALOGS, _context.Connection))
            {
                adapter.TableMappings.Add(BuildTableMapping(ds));

                adapter.Fill(ds);

                return ds;
            }
        }

        public RelationalDatabaseDataSet FindCatalogBy(CatalogName[] catalogNameCollection)
        {
            var ds = new RelationalDatabaseDataSet();
            
            using (var adapter = new SqlDataAdapter(CatalogStatements.SELECT_SINGLE_CATALOG, _context.Connection))
            {
                adapter.TableMappings.Add(BuildTableMapping(ds));

                foreach (CatalogName cat in catalogNameCollection)
                {
                    // Muda a base de dados actual associada à conexão
                    _context.Connection.ChangeDatabase(cat.Name);

                    adapter.Fill(ds);
                }
            }

            return ds;
        }

        // Mapeamento da tabela para a estrutura lógica do ADO.Net.
        // https://msdn.microsoft.com/en-us/library/ms810286.aspx
        // https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/dataadapter-datatable-and-datacolumn-mappings
        private DataTableMapping BuildTableMapping(RelationalDatabaseDataSet dataSet)
        {
            var dtm = new DataTableMapping("Table", dataSet.Catalog.TableName);
            // Este mapeamento permite alinhar com a estrutura de dados tipada do data set.
            dtm.ColumnMappings.Add("SERVER_NAME" /* campo da consulta */, dataSet.Catalog.ServerNameColumn.ColumnName);
            dtm.ColumnMappings.Add("CATALOG_ID", dataSet.Catalog.CatalogIdentifierColumn.ColumnName);
            dtm.ColumnMappings.Add("CATALOG_NAME", dataSet.Catalog.CatalogNameColumn.ColumnName);
            dtm.ColumnMappings.Add("CATALOG_CREATE_DATE", dataSet.Catalog.CatalogCreateDateColumn.ColumnName);
            dtm.ColumnMappings.Add("CATALOG_MODIFY_DATE", dataSet.Catalog.CatalogModifyDateColumn.ColumnName);
            return dtm;
        }       
    }
}
