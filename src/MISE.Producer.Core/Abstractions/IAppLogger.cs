using System.Runtime.CompilerServices;

namespace MISE.Producer.Core.Abstractions
{
    /// <summary>
    /// Abstracção do logging da aplicação.
    /// Desta forma, evita-se a dependência da assembly Microsof.Extensions.Logging.Abstractions
    /// na camada lógica
    /// </summary>
    /// <typeparam name="T">Tipo</typeparam>
    public interface IAppLogger<T>
    {
        void LogInformation(string message, params object[] args);
        void LogWarning(string message, params object[] args);
        void LogError(string message, params object[] args);
        void LogDebug(string message, params object[] args);
        void Trace(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0, params object[] args);
    }
}
