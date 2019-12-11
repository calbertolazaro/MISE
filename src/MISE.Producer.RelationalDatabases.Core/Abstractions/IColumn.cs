using MISE.Producer.Core.RelationalDatabases.Schema;

namespace MISE.Producer.Core.RelationalDatabases.Abstractions
{
    /// <summary>
    /// Interface do padrão módulo-de-tabela "Column"
    /// </summary>
    internal interface IColumn
    {
        /// <summary>
        /// Obtém todas as colunas de um conjunto de catálogos
        /// </summary>
        /// <param name="dataSet"></param>
        /// <param name="columnGateway"></param>
        /// <param name="catalogNameCollection"></param>
        /// <returns>Data Set</returns>
        void FillBy(RelationalDatabaseDataSet dataSet, IColumnGateway columnGateway, CatalogName[] catalogNameCollection);
    }
}