using Ardalis.EFCore.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MISE.MetadataRegistry.Core.DataCatalogAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISE.MetadataRegistry.Infrastructure.Data
{
    public class MetadataRegistryContext : DbContext
    {
        //private readonly IMediator _mediator;

        public MetadataRegistryContext(DbContextOptions<MetadataRegistryContext> options) : base(options)
        {
            //_mediator = mediator;
        }
        
        public DbSet<DataCatalog> Catalog { get; set; }
        public DbSet<Agent> Agent { get; set; }
        public DbSet<DataSet> DataSet{ get; set; }
        public DbSet<Distribution> Distribution { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .LogTo(Console.WriteLine, new[] { RelationalEventId.CommandExecuted })
                .EnableSensitiveDataLogging()
                ;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyAllConfigurationsFromCurrentAssembly();

            // alternately this is built-in to EF Core 2.2
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        
    }
}
