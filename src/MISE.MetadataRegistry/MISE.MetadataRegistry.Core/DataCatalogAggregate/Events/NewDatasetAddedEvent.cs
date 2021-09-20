using MISE.MetadataRegistry.Core.SharedKernel;

namespace MISE.MetadataRegistry.Core.DataCatalogAggregate.Events
{
    public class NewDatasetAddedEvent : BaseDomainEvent
    {
        public DataSet NewDataSet { get; set; }
        public DataCatalog DataCatalog { get; set; }

        public NewDatasetAddedEvent(DataCatalog dataCatalog, DataSet newDataSet)
        {
            DataCatalog = dataCatalog;
            NewDataSet = newDataSet;
        }
    }
}
