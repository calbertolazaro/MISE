using Microsoft.Extensions.Logging;
using MISE.SharedKernel.Abstractions;
using System.Runtime.CompilerServices;
using System.Text;

namespace MISE.SharedKernel.Logging
{       
    /// <summary>
    /// Adaptador do logging da aplicação para a infraestrutura de logging do .NET Core
    /// </summary>
    /// <typeparam name="T">Tipo</typeparam>
    public class LoggerAdapter<T> : IAppLogger<T>
    {
        private readonly ILogger<T> _logger;

        public LoggerAdapter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<T>();
        }

        public void LogWarning(string message, params object[] args)
        {
            _logger.LogWarning(message, args);
        }
        public void LogInformation(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
        }
        public void LogError(string message, params object[] args)
        {
            _logger.LogError(message, args);
        }
        public void LogDebug(string message, params object[] args)
        {
            _logger.LogDebug(message, args);
        }

        public void Trace(string message, [CallerMemberName] string methodName = "",
            [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0, params object[] args)
        {
            _logger.LogDebug($"{message} {sourceFilePath}:{sourceLineNumber},{methodName}({ArgsToString(args)})");
        }
        private string ArgsToString(object[] args)
        {
            if (args == null)
                return string.Empty;

            var argStringBuilder = new StringBuilder();
            return argStringBuilder.AppendJoin(",", args).ToString();
        }
    }
}
