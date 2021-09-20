using Ardalis.Specification.EntityFrameworkCore;
using MISE.MetadataRegistry.Core.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISE.MetadataRegistry.Infrastructure.Data
{
    public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
    {
        public EfRepository(MetadataRegistryContext dbContext) : base(dbContext)
        {
        }
    }
}
