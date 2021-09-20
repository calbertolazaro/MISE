using Ardalis.GuardClauses;
using MISE.MetadataRegistry.Core.DataCatalogAggregate.Events;
using MISE.MetadataRegistry.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISE.MetadataRegistry.Core.DataCatalogAggregate
{
    /// <summary>
    /// Representa um conjunto de dados publicado por um único agente e disponível para acesso num ou mais formatos
    /// </summary>
    public class DataSet: BaseEntity
    {
        /// <summary>
        /// Esta propriedade indica o nome dado ao conjunto de dados
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Esta propriedade contém um texto livre acerca do conjunto de dados 
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Esta propriedade contém o dia e hora em que o conjunto de dados foi formalmente publicado
        /// </summary>
        public DateTime ReleaseDate { get; private set; }

        /// <summary>
        /// Esta propriedade refere-se ao dia e hora em que o conjunto de dados foi modificado
        /// </summary>
        public DateTime ModificationDate { get; private set; }

        /// <summary>
        /// Esta propriedade indica a frequência com que o conjunto de dados é actualizado. É escolhido um termo da lista.
        /// </summary>
        public FrequencyType Frequency { get; private set; }

        /// <summary>
        /// Esta propriedade indica a frequência com que o conjunto de dados é actualizado quando não haja um termo aplicável
        /// </summary>
        public string OtherFrequency { get; private set; }

        private List<Distribution> _distribution = new List<Distribution>();
        public IEnumerable<Distribution> Distributions => _distribution.AsReadOnly();

        public DataCatalog Catalogue { get; private set; }
        
        public DataSet(string title, string description)
        {
            Title = Guard.Against.NullOrEmpty(title, nameof(title));
            Description = Guard.Against.NullOrEmpty(description, nameof(description));
        }

        public void AddDistribution(Distribution distribution)
        {
            Guard.Against.Null(distribution, nameof(distribution));
            _distribution.Add(distribution);

            var newDistributionAddedEvent = new NewDistributionAddedEvent(this, distribution);
            Events.Add(newDistributionAddedEvent);
        }

        public void ChangeDataSetReleaseDate(DateTime releaseDate)
        {
            ReleaseDate = Guard.Against.Null(releaseDate, nameof(releaseDate));
        }

        public void ChangeDataSetModifiedDate(DateTime modifiedDate)
        {
            ModificationDate = Guard.Against.Null(modifiedDate, nameof(modifiedDate));
        }

        public void ChangeDataSetFrequency(FrequencyType frequency, string otherFrequency = null)
        {
            Frequency = Guard.Against.Null(frequency, nameof(frequency));
            if (frequency == FrequencyType.Other)
                Guard.Against.Null(otherFrequency, nameof(otherFrequency));
            OtherFrequency = otherFrequency;
        }
    }
}
