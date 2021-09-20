using Ardalis.Specification;

namespace MISE.MetadataRegistry.Core.SharedKernel.Interfaces
{
    public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
    {
    }
}