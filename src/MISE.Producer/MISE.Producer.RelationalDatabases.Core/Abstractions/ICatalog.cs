using MISE.Producer.Core.RelationalDatabases.Schema;

namespace MISE.Producer.Core.RelationalDatabases.Abstractions
{
    /// <summary>
    /// Interface módulo-de-tabela "Catalog"
    /// </summary>
    internal interface ICatalog
    {
        /// <summary>
        /// Obtém todos os catálogos do SGBDR. Dado que o volume de dados pode gerar um problema de desempenho,
        /// não são obtidos as tabelas, colunas e procedimentos associados.
        /// </summary>
        /// <param name="catalogGateway">Gateway de acesso ao engine SQL</param>
        /// <returns>Data set</returns>
        RelationalDatabaseDataSet GetAll(ICatalogGateway catalogGateway);

        /// <summary>
        /// Obtém um novo data set apenas com alguns catálogos especificados pelo identificador "Nome"
        /// </summary>
        /// <param name="catalogGateway">Gateway de acesso ao engine SQL</param>
        /// <param name="catalogNameCollection"></param>
        /// <returns>Data Set</returns>
        RelationalDatabaseDataSet GetBy(ICatalogGateway catalogGateway, CatalogName[] catalogNameCollection);

        /// <summary>
        /// Preenche um data set existente com alguns catálogos especificados pelo identificador "Nome"
        /// </summary>
        /// <param name="dataSet">Data set</param>
        /// <param name="gateway">Gateway de acesso ao engine SQL</param>
        /// <param name="catalogNameCollection">Colecção com identificador do catálogo</param>
        void FillBy(RelationalDatabaseDataSet dataSet, ICatalogGateway gateway, CatalogName[] catalogNameCollection);
    }
}