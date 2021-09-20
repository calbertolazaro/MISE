using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MISE.MetadataRegistry.Core.DataCatalogAggregate;

namespace MISE.MetadataRegistry.Infrastructure.Data.Config
{
    public class AgentConfiguration : IEntityTypeConfiguration<Agent>
    {
        public void Configure(EntityTypeBuilder<Agent> builder)
        {
            builder
                .Property(p => p.Name)
                .HasMaxLength(128)
                .IsRequired()
                .IsUnicode();
                
        }
    }
}
