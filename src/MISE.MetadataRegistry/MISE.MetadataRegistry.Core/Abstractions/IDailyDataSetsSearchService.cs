using Ardalis.Result;
using MISE.MetadataRegistry.Core.DataCatalogAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MISE.MetadataRegistry.Core.Interfaces
{
    public interface IDailyDataSetsSearchService
    {
        //Task<Result<DataSet>> GetNextIncompleteItemAsync(int projectId);
        Task<Result<List<DataSet>>> GetAllDailyDataSetsAsync(int dataSetId, string searchString);
    }
}
