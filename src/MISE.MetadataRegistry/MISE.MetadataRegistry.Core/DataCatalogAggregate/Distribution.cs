using Ardalis.GuardClauses;
using MISE.MetadataRegistry.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISE.MetadataRegistry.Core.DataCatalogAggregate
{
    public class Distribution: BaseEntity
    {
        public string Title { get; private set; }

        public string Description { get; private set; }

        public string AccessURL { get; private set; }

        public DateTime ReleaseDate { get; private set; }

        public DateTime ModificationDate { get; private set; }

        public Distribution(string title, string description)
        {
            Title = Guard.Against.NullOrEmpty(title, nameof(title));
            Description = Guard.Against.NullOrEmpty(description, nameof(description));
        }

        public void ChangeDistributionReleasedDate(DateTime releaseDate)
        {
            ReleaseDate = Guard.Against.Null(releaseDate, nameof(releaseDate));
        }

        public void ChangeDistributionModificationDate(DateTime modifiedDate)
        {
            ModificationDate = Guard.Against.Null(modifiedDate, nameof(modifiedDate));
        }
    }
}
