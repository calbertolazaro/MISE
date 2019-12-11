using System;
using MISE.Producer.Core.RelationalDatabases.Abstractions;
using MISE.Producer.Core.RelationalDatabases.Attributes;
using MISE.Producer.Core.RelationalDatabases.Schema;

namespace MISE.Producer.Infrastructure.RelationalDatabases.Oracle
{
    [RelationalDatabaseGatewayMetadata(Title = "Oracle", Description = "Gateway de acesso aos metadados do Servidor de Oracle", Creator = "Carlos Lázaro", Publisher = "MISE")]
    public class OracleGateway : IRelationalDatabaseGateway
    {
        public RelationalDatabaseDataSet FindAllCatalogs()
        {
            throw new NotImplementedException();
        }

        public RelationalDatabaseDataSet FindCatalogBy(CatalogName[] catalogNameCollection)
        {
            throw new NotImplementedException();
        }

        public RelationalDatabaseDataSet FindTablesBy(CatalogName[] catalogNameCollection)
        {
            throw new NotImplementedException();
        }

        public RelationalDatabaseDataSet FindTablesBy(CatalogName catalog, TableName[] tableNameCollection)
        {
            throw new NotImplementedException();
        }

        public RelationalDatabaseDataSet FindColumnsBy(CatalogName[] catalogNameCollection)
        {
            throw new NotImplementedException();
        }

        public void FillColumnsBy(RelationalDatabaseDataSet dataSet, CatalogName[] catalogNameCollection)
        {
            throw new NotImplementedException();
        }

        public void Configure(string connectionString)
        {
            throw new NotImplementedException();
        }

        public RelationalDatabaseDataSet FindServer()
        {
            throw new NotImplementedException();
        }

        public void FillDomainsBy(RelationalDatabaseDataSet dataSet, CatalogName[] catalogNameCollection)
        {
            throw new NotImplementedException();
        }
    }
}
