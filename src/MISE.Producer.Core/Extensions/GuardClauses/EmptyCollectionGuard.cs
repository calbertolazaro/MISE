using System;

// Usar o mesmo namespace permite a facilidade de utilização das cláusulas
// de protecção de código na solução.
namespace Ardalis.GuardClauses
{
    public static class EmptyCollectionGuard
    {
        public static void NullOrEmptyCollection<T>(this IGuardClause guardClause, 
            T[] collection, string parameterName)
        {
            Guard.Against.Null(collection, parameterName);
            if (collection.Length == 0)
                throw new ArgumentException($"A colecção {parameterName} tem que ter elementos");
        }
    }
}

