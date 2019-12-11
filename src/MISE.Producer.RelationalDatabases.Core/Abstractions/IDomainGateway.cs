using MISE.Producer.Core.RelationalDatabases.Schema;

namespace MISE.Producer.Core.RelationalDatabases.Abstractions

{
    public interface IDomainGateway
    {
        void FillDomainsBy(RelationalDatabaseDataSet dataSet, CatalogName[] catalogNameCollection);
    }
}