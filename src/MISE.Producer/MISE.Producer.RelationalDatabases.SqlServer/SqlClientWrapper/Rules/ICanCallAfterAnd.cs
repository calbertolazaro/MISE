using System.Collections.Generic;

namespace MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.SqlClientWrapper.Rules
{
    public interface ICanCallAfterAnd
    {
        ICanCallAfterIsEqualTo IsEqualTo<T>(T value);
        ICanCallAfterIsNotEqualTo IsNotEqualTo<T>(T value);
        ICanCallAfterIn In<T>(IEnumerable<T> value);
    }
}