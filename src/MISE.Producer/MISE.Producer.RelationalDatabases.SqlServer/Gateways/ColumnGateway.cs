using System.Data.Common;
using System.Linq;
using Ardalis.GuardClauses;
using MISE.Producer.Core.RelationalDatabases.Abstractions;
using MISE.Producer.Core.RelationalDatabases.Schema;
using MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.Abstractions;
using MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.SqlClientWrapper;
using MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.SqlStatements;

namespace MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.Gateways
{
    internal class ColumnGateway : IColumnGateway
    {
        readonly IGatewayContext _context;
        internal ColumnGateway(IGatewayContext context)
        {
            Guard.Against.Null(context, nameof(context));
            _context = context;
        }
        
        public RelationalDatabaseDataSet FindColumnsBy(CatalogName[] catalogNameCollection)
        {
            Guard.Against.NullOrEmptyCollection(catalogNameCollection, nameof(catalogNameCollection));

            RelationalDatabaseDataSet  results = new RelationalDatabaseDataSet();
            FillColumnsBy(results, catalogNameCollection);
            return results;
        }

        public void FillColumnsBy(RelationalDatabaseDataSet dataSet, CatalogName[] catalogNameCollection)
        {
            var dsl = Dsl.ConnectToMsSqlServer().UsingThisConnectionString(_context.ConnectionString);

            dsl.ForEachCatalog(catalogNameCollection.Select(c => c.Name).ToArray())
                .SelectAllColumns()
                .From(ColumnStatements.FromStatement)
                //.Where("TABLE_CATALOG").IsEqualTo(catalogName)
                //.And("TABLE_NAME").In(tableNameCollection)
                .OrderBy("TABLE_CATALOG")
                .ThenBy("TABLE_SCHEMA").Asc()
                .ThenBy("TABLE_NAME").Asc()
                .ThenBy("ORDINAL_POSITION").Asc()
                .Adapt()
                .UsingThisMapping(BuildTableMapping(dataSet))
                .FillDataset(dataSet);
        }


        // Mapeamento da tabela para a estrutura lógica do ADO.Net: https://msdn.microsoft.com/en-us/library/ms810286.aspx (consultado a 25.7.2018)
        private DataTableMapping BuildTableMapping(RelationalDatabaseDataSet ds)
        {
            var dtm = new DataTableMapping
            {
                SourceTable = "Table",
                DataSetTable = ds.Column.TableName
            };

            dtm.ColumnMappings.Add("TABLE_SERVER_NAME", ds.Column.ServerNameColumn.ColumnName);
            dtm.ColumnMappings.Add("TABLE_CATALOG_ID", ds.Column.CatalogIdentifierColumn.ColumnName);
            dtm.ColumnMappings.Add("TABLE_CATALOG", ds.Column.CatalogNameColumn.ColumnName);
            dtm.ColumnMappings.Add("TABLE_SCHEMA", ds.Column.SchemaNameColumn.ColumnName);
            dtm.ColumnMappings.Add("TABLE_NAME", ds.Column.TableNameColumn.ColumnName);
            dtm.ColumnMappings.Add("COLUMN_NAME", ds.Column.ColumnNameColumn.ColumnName);
            dtm.ColumnMappings.Add("ORDINAL_POSITION", ds.Column.OrdinalPositionColumn.ColumnName);
            dtm.ColumnMappings.Add("DOMAIN_SCHEMA", ds.Column.DomainSchemaNameColumn.ColumnName);
            dtm.ColumnMappings.Add("DOMAIN_NAME", ds.Column.DomainNameColumn.ColumnName);
            dtm.ColumnMappings.Add("DATA_TYPE", ds.Column.DataTypeColumn.ColumnName);
            dtm.ColumnMappings.Add("CHARACTER_MAXIMUM_LENGTH", ds.Column.CharacterMaximumLengthColumn.ColumnName);
            dtm.ColumnMappings.Add("NUMERIC_PRECISION", ds.Column.NumericPrecisionColumn.ColumnName);
            dtm.ColumnMappings.Add("NUMERIC_SCALE", ds.Column.NumericScaleColumn.ColumnName);
            dtm.ColumnMappings.Add("COLUMN_DEFAULT", ds.Column.ColumnDefaultColumn.ColumnName);
            dtm.ColumnMappings.Add("IS_NULLABLE", ds.Column.IsNullableColumn.ColumnName);
            dtm.ColumnMappings.Add("IS_PRIMARY_KEY", ds.Column.IsPrimaryKeyColumn.ColumnName);
            dtm.ColumnMappings.Add("IS_FOREIGN_KEY", ds.Column.IsForeignKeyColumn.ColumnName);
        
            return dtm;
        }
    }
}
