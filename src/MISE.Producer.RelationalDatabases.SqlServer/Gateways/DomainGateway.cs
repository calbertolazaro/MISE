using System;
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
    class DomainGateway : IDomainGateway
    {
        readonly IGatewayContext _context;
        internal DomainGateway(IGatewayContext context)
        {
            Guard.Against.Null(context, nameof(context));
            _context = context;
        }

        public void FillDomainsBy(RelationalDatabaseDataSet dataSet, CatalogName[] catalogNameCollection)
        {


            foreach (var catalog in catalogNameCollection)
            {
                Dsl.ConnectToMsSqlServer().UsingThisConnectionString(_context.ConnectionString)
                    .ForThisCatalog(catalog)
                    .SelectAllColumns()
                    .From(DomainStatements.FromStatement)
                    .Where("DOMAIN_SCHEMA").IsNotEqualTo("sys")
                    .OrderBy("DOMAIN_SCHEMA").Asc().ThenBy("DOMAIN_NAME").Asc()
                    .Adapt().UsingThisMapping(BuildTableMapping(dataSet))
                    .FillDataset(dataSet);
            }
            
            Dsl.ConnectToMsSqlServer().UsingThisConnectionString(_context.ConnectionString)
                .ForThisCatalog(catalogNameCollection.First())
                .SelectAllColumns()
                .From(DomainStatements.FromStatement)
                .Where("DOMAIN_SCHEMA").IsEqualTo("sys")
                .OrderBy("DOMAIN_SCHEMA").Asc().ThenBy("DOMAIN_NAME").Asc()
                .Adapt().UsingThisMapping(BuildTableMapping(dataSet))
                .FillDataset(dataSet);
        }

        private DataTableMapping BuildTableMapping(RelationalDatabaseDataSet ds)
        {
            var dtm = new DataTableMapping
            {
                SourceTable = "Table",
                DataSetTable = ds.Domain.TableName
            };

            dtm.ColumnMappings.Add("DOMAIN_CATALOG_ID", ds.Domain.CatalogIdentifierColumn.ColumnName);

            dtm.ColumnMappings.Add("DOMAIN_CATALOG", ds.Domain.CatalogNameColumn.ColumnName);
            dtm.ColumnMappings.Add("DOMAIN_SCHEMA", ds.Domain.SchemaNameColumn.ColumnName);
            dtm.ColumnMappings.Add("DOMAIN_NAME", ds.Domain.DomainNameColumn.ColumnName);
            dtm.ColumnMappings.Add("DATA_TYPE",ds.Domain.DataTypeColumn.ColumnName);
            dtm.ColumnMappings.Add("NUMERIC_PRECISION",ds.Domain.NumericPrecisionColumn.ColumnName);
            dtm.ColumnMappings.Add("NUMERIC_SCALE", ds.Domain.NumericScaleColumn.ColumnName);

            return dtm;
        }
    }
}
