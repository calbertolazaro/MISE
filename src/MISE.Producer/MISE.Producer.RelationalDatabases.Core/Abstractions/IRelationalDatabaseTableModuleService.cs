using MISE.Producer.Core.RelationalDatabases.TableModule;

namespace MISE.Producer.Core.RelationalDatabases.Abstractions
{
    /// <summary>
    /// Abstracção para agregar os "módulos de tabela"
    /// </summary>
    internal interface IRelationalDatabaseTableModuleService
    {
        ICatalog Catalog { get; }
        ITable Table { get; }
        IColumn Column { get; }
        IServer Server { get; }
        IDomain Domain { get; }
    }
}