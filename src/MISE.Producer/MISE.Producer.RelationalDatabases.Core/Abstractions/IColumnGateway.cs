using System;
using Ardalis.GuardClauses;
using MISE.Producer.Core.RelationalDatabases.Schema;

namespace MISE.Producer.Core.RelationalDatabases.Abstractions
{
    /// <summary>
    /// Abstracção das consultas de SQL à tabela/vista "Columns" (padrão table-gateway)
    /// </summary>
    public interface IColumnGateway
    {
        /// <summary>
        /// Selecciona todas as colunas de um ou mais catálogos
        /// </summary>
        /// <param name="catalogNameCollection">Colecção de identificadores </param>
        RelationalDatabaseDataSet FindColumnsBy(CatalogName[] catalogNameCollection);

        /// <summary>
        /// Selecciona todas as colunas de um ou mais catálogos
        /// </summary>
        /// <param name="catalogNameCollection">Colecção de identificadores </param>
        void FillColumnsBy(RelationalDatabaseDataSet dataSet, CatalogName[] catalogNameCollection);
    }

    public struct TableName
    {
        public string Name { get; }

        public TableName(string tableName)
        {
            Name = tableName;
        }

        // Conversão implicita para o tipo 'string'
        public static implicit operator string(TableName c) => c.Name;

        // Conversão explicita para o tipo 'string'
        public static explicit operator TableName(string name)
        {   
            return new TableName(name);
        }

        public static TableName[] FromArray(string[] tableNameCollection)
        {
            Guard.Against.Null(tableNameCollection, nameof(tableNameCollection));
            return Array.ConvertAll(tableNameCollection, t => new TableName(t));
        }

        public static string[] ToArray(TableName[] tableNameCollection)
        {
            return Array.ConvertAll(tableNameCollection, t => t.Name);
        }
    }
}