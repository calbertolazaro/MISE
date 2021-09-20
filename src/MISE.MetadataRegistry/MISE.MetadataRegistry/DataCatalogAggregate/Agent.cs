using Ardalis.GuardClauses;
using MISE.MetadataRegistry.Core.SharedKernel;

namespace MISE.MetadataRegistry.Core.DataCatalogAggregate
{
    /// <summary>
    /// Representa uma pessoa ou entidade relacionada com o catálogo/conjunto
    /// </summary>
    public class Agent : BaseEntity
    {
        public string Name { get; private set; }

        public Agent(string name)
        {
            Name = Guard.Against.NullOrEmpty(name, nameof(name));
        }
    }

    
}
