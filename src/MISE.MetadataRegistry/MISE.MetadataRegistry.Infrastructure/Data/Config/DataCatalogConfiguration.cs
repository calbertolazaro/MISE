using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MISE.MetadataRegistry.Core.DataCatalogAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISE.MetadataRegistry.Infrastructure.Data.Config
{
    public class DataCatalogConfiguration : IEntityTypeConfiguration<DataCatalog>
    {
        public void Configure(EntityTypeBuilder<DataCatalog> builder)
        {
            builder.Property(p => p.Title)
                .HasMaxLength(128)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.Description)
                .HasMaxLength(1024)
                .IsRequired()
                .IsUnicode();

            //builder.Property(p => p.ReleaseDate)
            //    .IsRequired();

            //builder.Property(p => p.ModificationDate)
            //.IsRequired();

            
        }
    }
}
