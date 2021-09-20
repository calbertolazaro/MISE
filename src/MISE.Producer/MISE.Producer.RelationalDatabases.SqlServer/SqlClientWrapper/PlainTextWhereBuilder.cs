using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;

namespace MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.SqlClientWrapper
{
    public class PlainTextWhereBuilder : IWhereBuilder
    {
        private string _commandText = string.Empty;
        public string BuildWhere(IReadOnlyCollection<WhereCondition> whereConditions)
        {
            if (whereConditions.Count == 0)
                return string.Empty;

            StringBuilder strWhereBuilder = new StringBuilder();

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
                            string value = condition.Value is String
                                ? $"'{condition.Value}'"
                                : Convert.ToString(condition.Value, CultureInfo.InvariantCulture);
                            string strEqualTo = $"{currentPredicate} {condition.ColumnName} = {value}";
                            strWhereBuilder.Append(strEqualTo);
                            break;
                        }

                    case ComparisonMethod.NotEqualTo:
                        {
                            string value = condition.Value is String ? $"'{condition.Value}'" : Convert.ToString(condition.Value, CultureInfo.InvariantCulture);
                            string stringNotEqualTo = $"{currentPredicate} {condition.ColumnName} <> {value}";
                            strWhereBuilder.Append(stringNotEqualTo);
                            break;
                        }

                    case ComparisonMethod.In:
                        {
                            // AND ColunaC IN (1, 2, 3)
                            IEnumerable values = (IEnumerable)condition.Value;
                            StringBuilder inclause = new StringBuilder();
                            foreach (object o in values)
                            {
                                inclause.Append(o is String ? $"'{o}'" : Convert.ToString(o, CultureInfo.InvariantCulture));
                                inclause.Append(", ");
                            }

                            inclause.Remove(inclause.Length - 2, 2);

                            string strIn = $"{currentPredicate} {condition.ColumnName} IN ({inclause.ToString()})";
                            strWhereBuilder.Append(strIn);
                            break;
                        }

                    default:
                        throw new ArgumentException($"O comparador {condition.Comparator} não é reconhecido");
                }

                // O próximo elemento reafectará a variável.
                currentPredicate = "AND";
            }

            _commandText = strWhereBuilder.ToString();

            return _commandText;
        }

        public void PrepareCommand(IDbCommand command)
        {
            command.CommandType = CommandType.Text;
            command.CommandText = _commandText;
        }
    }
}