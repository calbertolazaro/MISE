namespace MISE.Producer.Core.RelationalDatabases.Abstractions
{
    /// <summary>
    /// Abstracção da camada de acesso aos dados (padrão Table Data Gateway).
    /// A implementação pertence à camada de infraestrutura.
    /// </summary>
    public interface IRelationalDatabaseGateway : 
        ICatalogGateway, 
        ITableGateway, 
        IColumnGateway,
        IServerGateway,
        IDomainGateway
    {
        void Configure(string connectionString);
    }
}
