using System;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using MISE.Producer.Core.RelationalDatabases.Schema;

namespace MISE.Producer.Core.RelationalDatabases.Abstractions
{
    /// <summary>
    /// Abstracção das consultas de SQL à tabela/vista "Catalogs" (padrão table-gateway)
    /// </summary>
    public interface ICatalogGateway
    {
        /// <summary>
        /// Obter todos os catálogos do sistema base de dados
        /// </summary>
        RelationalDatabaseDataSet FindAllCatalogs();

        /// <summary>
        /// Obter catálogos por nome
        /// </summary>
        /// <param name="ds">DataSet</param>
        /// <param name="catalogNameCollection"></param>
        RelationalDatabaseDataSet FindCatalogBy(CatalogName[] catalogNameCollection);
    }

    #region Identificadores

    /// <summary>
    /// Nome do catálogo
    /// </summary>
    public struct CatalogName
    {
        public string Name { get; }

        public CatalogName(string catalogName)
        {
            Name = catalogName;
        }

        // Conversão implicita para o tipo 'string'
        public static implicit operator string(CatalogName c) => c.Name;
        
        // Conversão explicita para o tipo 'string'
        public static explicit operator CatalogName(string name)
        {
            return new CatalogName(name);
        }

        public static string[] ToArray(CatalogName[] catalogNameCollection)
        {
            return Array.ConvertAll(catalogNameCollection, c => (string) c);
        }

        public static CatalogName[] FromArray(string[] catalogNameCollection)
        {
            return Array.ConvertAll(catalogNameCollection, c => (CatalogName) c);
        }
    }

    #endregion
}