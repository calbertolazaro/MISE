using MISE.Producer.Core.RelationalDatabases.Schema;

namespace MISE.Producer.Core.RelationalDatabases.Abstractions
{
    internal interface IDomain
    {
        void FillBy(RelationalDatabaseDataSet dataSet, IDomainGateway gateway, CatalogName[] catalogNameCollection);
    }
}