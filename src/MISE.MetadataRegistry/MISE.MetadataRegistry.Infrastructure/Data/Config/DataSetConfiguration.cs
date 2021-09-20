using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MISE.MetadataRegistry.Core.DataCatalogAggregate;
using System;

namespace MISE.MetadataRegistry.Infrastructure.Data.Config
{
    public class DataSetConfiguration : IEntityTypeConfiguration<DataSet>
    {
        public void Configure(EntityTypeBuilder<DataSet> builder)
        {
            builder.Property(p => p.Title)
                .HasMaxLength(128)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.Description)
                .HasMaxLength(1024)
                .IsRequired()
                .IsUnicode();

            builder.Property(p => p.Frequency)
                .HasConversion(
                    v => v.ToString(), 
                    v => (FrequencyType)Enum.Parse(typeof(FrequencyType), v));

            builder.Property(p => p.OtherFrequency)
                .HasMaxLength(128)
                .IsUnicode();
        }
    }
}
