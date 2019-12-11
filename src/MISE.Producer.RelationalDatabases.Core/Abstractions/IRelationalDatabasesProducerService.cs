using MISE.Producer.Core.RelationalDatabases.Schema;

namespace MISE.Producer.Core.RelationalDatabases.Abstractions
{
    /// <summary>
    /// Abstracção para interacção com o back-end do sistema para obter metadata das bases de dados relacionais
    /// </summary>
    public interface IRelationalDatabasesProducerService
    {
        /// <summary>
        /// Obter dados de todos os catálogos de um Sistema de Gestão de Base de Dados Relacionais
        /// </summary>
        /// <param name="dataSource">Fonte de dados</param>
        /// <param name="connectionString">Parametrização da ligação à fonte de dados</param>
        /// <returns>Dataset com preenchimento parcial (Catalog)</returns>
        RelationalDatabaseDataSet GetCatalogs(string dataSource, string connectionString);

        /// <summary>
        /// Obter dados de alguns catálogos de um Sistema de Gestão de Base de Dados Relacionais
        /// </summary>
        /// <param name="dataSource">Fonte de dados</param>
        /// <param name="connectionString">Parametrização da ligação à fonte de dados</param>
        /// <param name="catalogNameCollection">Colecção de nomes dos catálogos</param>
        /// <returns>Dataset com preenchimento parcial (Catalog)</returns>
        RelationalDatabaseDataSet GetCatalogs(string dataSource, string connectionString, CatalogName[] catalogNameCollection);

        /// <summary>
        /// Obter todas as tabelas de um conjunto de base de dados
        /// </summary>
        /// <param name="dataSource">Fonte de dados</param>
        /// <param name="connectionString">Parametrização da ligação à fonte de dados</param>
        /// <param name="catalogNameCollection">Colecção de nomes dos catálogos</param>
        /// <returns></returns>
        RelationalDatabaseDataSet GetTables(string dataSource, string connectionString, CatalogName[] catalogNameCollection);

        /// <summary>
        /// Obter dados de das tabelas de uma colecção de catálogos.
        /// </summary>
        /// <param name="dataSource">Fonte de dados</param>
        /// <param name="connectionString">Parametrização da ligação à fonte de dados</param>
        /// <param name="catalogNameCollection">Colecção de nomes de catálogos</param>
        /// <param name="tableNameCollection">Colecção de nomes de tabelas</param>
        /// <returns>Dataset com preenchimento parcial (Table)</returns>
        RelationalDatabaseDataSet GetTables(string dataSource, string connectionString, CatalogName[] catalogNameCollection, TableName[] tableNameCollection);

        //RelationalDatabaseDataSet GetColumns(string gatewayTitle, string connectionString, ColumnName[] columnName);
        //RelationalDatabaseDataSet GetColumns(string gatewayTitle, string connectionString, ColumnName[] columnName);
        //RelationalDatabaseDataSet GetSchema(string gatewayTitle, string connectionString, CatalogName catalogName);
        //RelationalDatabaseDataSet GetSchema(string gatewayTitle, string connectionString, CatalogName catalogName, TableName tableName);
        //RelationalDatabaseDataSet GetSchema(string gatewayTitle, string connectionString, CatalogName catalogName, TableName tableName, ColumnName columnName);
        RelationalDatabaseDataSet GetSchema(string dataSource, string connectionString, CatalogName[] catalogNameCollection);
    }
}