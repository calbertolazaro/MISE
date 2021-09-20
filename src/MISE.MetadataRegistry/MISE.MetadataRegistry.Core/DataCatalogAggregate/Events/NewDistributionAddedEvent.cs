using MISE.MetadataRegistry.Core.SharedKernel;

namespace MISE.MetadataRegistry.Core.DataCatalogAggregate.Events
{
    public class NewDistributionAddedEvent : BaseDomainEvent
    {
        public Distribution NewDistribution { get; set; }
        public DataSet DataSet { get; set; }

        public NewDistributionAddedEvent(DataSet dataSet, Distribution newDistribution)
        {
            DataSet = dataSet;
            NewDistribution = newDistribution;
        }
    }
}
