using MISE.Producer.Core.RelationalDatabases.Schema;

namespace MISE.Producer.Core.RelationalDatabases.Abstractions
{
    /// <summary>
    /// Abstracção das consultas de SQL à tabela/vista "Tables" (padrão table-gateway)
    /// </summary>
    public interface ITableGateway
    {
        RelationalDatabaseDataSet FindTablesBy(CatalogName[] catalogNameCollection);
        RelationalDatabaseDataSet FindTablesBy(CatalogName catalog, TableName[] tableNameCollection);
    }
}