using System.Data;

namespace MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.SqlClientWrapper.Extensions
{
    /// <summary>
    /// Extensões à classe DbCommand.
    /// https://social.msdn.microsoft.com/Forums/en-US/d56a4710-3fd1-4039-a0d9-c4c6bd1cd22e/dbcommand-parameters-collection-missing-addwithvalue-method
    /// </summary>
    public static class IDbCommandExtensions
    {
        /// <summary>
        /// Facilitador de manipulação dos parâmetros
        /// </summary>
        /// <param name="command">Comando de BD</param>
        /// <param name="parameterName">Nome do parâmetro</param>
        /// <param name="parameterValue">Valor do parâmetro</param>
        public static void AddParameterWithValue(this IDbCommand command, string parameterName, object parameterValue)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = parameterValue;
            command.Parameters.Add(parameter);
        }    
    }
}