using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.SqlClientWrapper.Extensions;


namespace MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.SqlClientWrapper
{
    internal class DbCommandWhereBuilder : IWhereBuilder
    {
        private string _commandText = string.Empty;
        readonly List<string> _parameterNameList = new List<string>();
        readonly List<object> _parameterValueList = new List<object>();

        public string BuildWhere(IReadOnlyCollection<WhereCondition> whereConditions)
        {
            if (whereConditions.Count == 0)
                return string.Empty;

            StringBuilder strWhere = new StringBuilder();

            string currentPredicate = string.Empty;

            foreach (var condition in whereConditions)
            {
                // O predicado não é usado na primeira condição.
                // Necessário um espaço adicional para separar da condição anterior.
                if (!string.IsNullOrEmpty(currentPredicate))
                {
                    if (condition.Predicate == Predicate.And)
                        currentPredicate = " AND";
                    else
                        throw new ArgumentException($"O predicado {condition.Predicate} não é reconhecido");
                }

                switch (condition.Comparator)
                {
                    case ComparisonMethod.EqualTo:
                        {
                            string paramName = $"@p{condition.ColumnName}";
                            string strEqualTo = $"{currentPredicate} {condition.ColumnName} = {paramName}";
                            strWhere.Append(strEqualTo);
                            _parameterNameList.Add(paramName);
                            _parameterValueList.Add(condition.Value);
                            break;
                        }

                    case ComparisonMethod.NotEqualTo:
                        {
                            string paramName = $"@p{condition.ColumnName}";
                            string stringNotEqualTo = $"{currentPredicate} {condition.ColumnName} <> {paramName}";
                            strWhere.Append(stringNotEqualTo);
                            _parameterNameList.Add(paramName);
                            _parameterValueList.Add(condition.Value);
                            break;
                        }

                    case ComparisonMethod.In:
                        {
                            object[] values = (object[])condition.Value;
                            // Criação dos parâmetros usando a convenção @p<NomeDaColuna><Indice>
                            string[] paramNames = values.Select( (s, i) => $"@p{condition.ColumnName}{i}").ToArray();
                            string inClause = String.Join(", ", paramNames);                     // inClause = "@pColunaC0, @pColunaC1, @pColunaC3"
                            string strIn = $"{currentPredicate} {condition.ColumnName} IN ({inClause})"; //    strIn = "AND ColunaC IN (@pColunaC0, @pColunaC1, @pColunaC3)"
                            strWhere.Append(strIn);
                            for (int i = 0; i < values.Length; i++)
                            {
                                string parameterName = paramNames[i];
                                object parameterValue = values[i];
                                _parameterNameList.Add(parameterName);
                                _parameterValueList.Add(parameterValue);
                            }
                            break;
                        }

                    default:
                        throw new ArgumentException($"O comparador {condition.Comparator} não é reconhecido");
                }

                // O próximo elemento reafectará a variável.
                currentPredicate = "AND";
            }

            _commandText = strWhere.ToString();

            return _commandText;
        }

        public void PrepareCommand(IDbCommand command)
        {
            if (_parameterNameList.Count != _parameterValueList.Count)
                throw new NotSupportedException($"O número de elementos nas listas (nomes de parâmetros e valores) não coincidem");

            for (int i = 0; i < _parameterNameList.Count; i++)
            {
                command.AddParameterWithValue(_parameterNameList[i], _parameterValueList[i]);
            }
        }
    }
}