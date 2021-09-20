using Ardalis.GuardClauses;
using MISE.MetadataRegistry.Core.DataCatalogAggregate.Events;
using MISE.MetadataRegistry.Core.SharedKernel;
using MISE.MetadataRegistry.Core.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;

namespace MISE.MetadataRegistry.Core.DataCatalogAggregate
{
    /// <summary>
    /// Representa um catálogo de dados. Cada registo descreve uma colecção de datasets. O que constitui um conjunto de colecção de dados fica 
    /// ao critério do administrador do repositório. Por exemplo, podemos ter um catálogo de todos os ficheiros produzidos pelo CRM que são enviados
    /// para o data warehouse.
    /// </summary>
    public class DataCatalog : BaseEntity, IAggregateRoot
    {
        /// <summary>
        /// Esta propriedade indica o nome dado ao catálogo de dados
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Esta propriedade indica o nome dado ao catálogo de dado
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Esta propriedade contém o dia e hora em que o catálogo foi formalmente publicado
        /// </summary>
        public DateTime? ReleaseDate { get; private set; }

        /// <summary>
        /// Esta propriedade contém o dia e hora em que o catálogo foi modificado
        /// </summary>
        public DateTime? ModificationDate { get; private set; }

        /// <summary>
        /// Esta propriedade refere-se à entidade responsável por tornar o catálogo disponível
        /// </summary>
        public Agent Publisher { get; private set; }

        private List<DataSet> _dataSets = new List<DataSet>();
        /// <summary>
        /// Esta propriedade representa a ligação de um ou mais conjuntos de dados associados ao catálogo
        /// </summary>
        public IEnumerable<DataSet> DataSets => _dataSets.AsReadOnly();

        public DataCatalog(string title, string description)
        {
            Title = Guard.Against.NullOrEmpty(title, nameof(title));
            Description = Guard.Against.NullOrEmpty(description, nameof(description));
        }

        public void AddDataSet(DataSet dataSet)
        {
            Guard.Against.Null(dataSet, nameof(dataSet));
            _dataSets.Add(dataSet);

            var newDataSetAddedEvent = new NewDatasetAddedEvent(this, dataSet);
            Events.Add(newDataSetAddedEvent);
        }

        public void ChangedDataCatalogReleaseDate(DateTime releaseDate)
        {
            ReleaseDate = Guard.Against.Null(releaseDate, nameof(releaseDate));
        }

        public void ChangeDataCatalogModifiedDate(DateTime modifiedDate)
        {
            ModificationDate = Guard.Against.Null(modifiedDate, nameof(modifiedDate));
        }
    }
}
