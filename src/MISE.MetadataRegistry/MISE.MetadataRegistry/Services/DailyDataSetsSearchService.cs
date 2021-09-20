using Ardalis.Result;
using MISE.MetadataRegistry.Core.DataCatalogAggregate;
using MISE.MetadataRegistry.Core.Interfaces;
using MISE.MetadataRegistry.Core.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MISE.MetadataRegistry.Core.Services
{
    public class DailyDataSetsSearchService : IDailyDataSetsSearchService
    {
        private readonly IRepository<DataCatalog> _repository;

        public DailyDataSetsSearchService(IRepository<DataCatalog> repository)
        {
            _repository = repository;
        }
        public Task<Result<List<DataSet>>> GetAllDailyDataSetsAsync(int dataSetId, string searchString)
        {
            throw new NotImplementedException();
        }
    }
}
