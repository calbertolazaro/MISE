using MISE.Producer.Core.RelationalDatabases.Schema;

namespace MISE.Producer.Core.RelationalDatabases.Abstractions
{
    /// <summary>
    /// Interface padrão módulo-de-tabela "Table"
    /// </summary>
    internal interface ITable
    {
        /// <summary>
        /// Obtém algumas tabelas de um conjunto de catálogos
        /// </summary>
        /// <param name="tableGateway"></param>
        /// <param name="catalogNameCollection"></param>
        /// <param name="tableNameCollection"></param>
        /// <returns>Data Set</returns>
        RelationalDatabaseDataSet GetBy(ITableGateway tableGateway, CatalogName[] catalogNameCollection, TableName[] tableNameCollection);


        /// <summary>
        /// Obtém todas tabelas de um conjunto de catálogos
        /// </summary>
        /// <param name="tableGateway"></param>
        /// <param name="catalogNameCollection"></param>
        /// <param name="tableNameCollection"></param>
        /// <returns>Data Set</returns>
        RelationalDatabaseDataSet GetBy(ITableGateway tableGateway, CatalogName[] catalogNameCollection);

        /// <summary>
        /// Obtém todas as tabelas de um conjunto de catálogos
        /// </summary>
        /// <param name="dataSet"></param>
        /// <param name="tableGateway"></param>
        /// <param name="catalogNameCollection"></param>
        /// <returns>Data Set</returns>
        void FillBy(RelationalDatabaseDataSet dataSet, ITableGateway tableGateway, CatalogName[] catalogNameCollection);
    }
}