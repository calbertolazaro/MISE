using System.Collections.Generic;
using MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.SqlClientWrapper;
using MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.SqlClientWrapper.Rules;
using Xunit;

namespace MISE.Producer.Tests.Infrastructure.RelationalDatabases.SqlClientWrapper
{
    public class DslTests
    {
        // Conjunto de testes para validar a correcção do statement de SQL
        public class SqlBuilder
        {
            private static readonly string TableName = "TABELA";
            private static readonly string DefaultFrom = $"FROM ({TableName}) AS {Dsl.DefaultAlias}";
            private static readonly string DefaultFromWithoutAlias = $"FROM {TableName}";
            private static readonly string DefaulSelectFromWithWhere = $"SELECT * {DefaultFrom} WHERE ";

            [Fact]
            public void ProduceValidSqlQuery_With_SelectAllColumnsFromATable()
            {
                ICanCallAfterSqlBuilder builder = Dsl.PlainSql();

                string result = builder.SelectAllColumns().From(TableName).ToSql();

                Assert.Equal($"SELECT * {DefaultFrom}", result);
            }

            [Fact]
            public void ProduceValidSqlQuery_With_SelectAllColumnsFromATableWithoutAlias()
            {
                ICanCallAfterSqlBuilder builder = Dsl.PlainSql();

                string result = builder.SelectAllColumns().From(TableName).WithoutAlias().ToSql();

                Assert.Equal($"SELECT * {DefaultFromWithoutAlias}", result);
            }

            [Theory,
             InlineData(new string[] { "Coluna1" }, "TabelaX", "SELECT Coluna1 FROM (TabelaX) AS DslTable"),
             InlineData(new string[] { "Coluna1", "Coluna2" }, "TabelaY", "SELECT Coluna1, Coluna2 FROM (TabelaY) AS DslTable"),
             InlineData(new string[] { "Coluna1", "Coluna2", "Coluna3" }, "TabelaZ", "SELECT Coluna1, Coluna2, Coluna3 FROM (TabelaZ) AS DslTable")
            ]
            public void ProduceValidSqlQuery_With_SelectSpecificColumnUsingDefaultFrom(IEnumerable<string> inputColumns, string tableName, string expected)
            {
                ICanCallAfterSqlBuilder builder = Dsl.PlainSql();
                ICanCallAfterSelectColumn afterSelectColumn = null;

                foreach (string column in inputColumns)
                    afterSelectColumn = builder.SelectColumn(column);

                string result = afterSelectColumn.From(tableName).ToSql();
                Assert.Equal(expected, result);
            }

            [Theory,
             InlineData(new string[] { "Coluna1" }, "TabelaX", "SELECT Coluna1 FROM TabelaX"),
             InlineData(new string[] { "Coluna1", "Coluna2" }, "TabelaY", "SELECT Coluna1, Coluna2 FROM TabelaY"),
             InlineData(new string[] { "Coluna1", "Coluna2", "Coluna3" }, "TabelaZ", "SELECT Coluna1, Coluna2, Coluna3 FROM TabelaZ")
            ]
            public void ProduceValidSqlQuery_With_SelectSpecificColumnUsingFromWithoutAlias(IEnumerable<string> inputColumns, string inputTable, string expected)
            {
                ICanCallAfterSqlBuilder builder = Dsl.PlainSql();
                ICanCallAfterSelectColumn afterSelectColumn = null;

                foreach (string column in inputColumns)
                    afterSelectColumn = builder.SelectColumn(column);

                string result = afterSelectColumn.From(inputTable).WithoutAlias().ToSql();

                Assert.Equal($"{expected}", result);
            }


            [Theory,
            InlineData("ColunaA", 10, "ColunaA = 10"),
            InlineData("ColunaB", 20, "ColunaB = 20")
            ]
            public void ProduceValidSqlQuery_WithWhereClause_NumericEqual(string inputColumn, int inputValue, string expected)
            {
                ICanCallAfterSqlBuilder builder = Dsl.PlainSql();

                string result = builder.SelectAllColumns().From(TableName).Where(inputColumn).IsEqualTo(inputValue).ToSql();

                Assert.Equal($"{DefaulSelectFromWithWhere} {expected}", result);
            }

            [Theory,
             InlineData("ColunaC", "texto", "ColunaC = 'texto'"),
             InlineData("ColunaD", "outro", "ColunaD = 'outro'")
            ]
            public void ProduceValidSqlQuery_WithWhereClause_AlphanumericEqual(string inputColumn, string inputValue, string expected)
            {
                ICanCallAfterSqlBuilder builder = Dsl.PlainSql();

                string result = builder.SelectAllColumns().From(TableName).Where(inputColumn).IsEqualTo(inputValue).ToSql();

                Assert.Equal($"{DefaulSelectFromWithWhere} {expected}", result);
            }

            [Theory,
             InlineData("ColunaE", new[] { 1 }, "ColunaE IN (1)"),
             InlineData("ColunaD", new[] { 1, 2 }, "ColunaD IN (1, 2)"),
             InlineData("ColunaF", new[] { 1, 2, 3 }, "ColunaF IN (1, 2, 3)")
            ]
            public void ProduceValidSqlQuery_WithWhereClause_NumericIn(string inputColumn, IEnumerable<int> inputValue, string expected)
            {
                ICanCallAfterSqlBuilder builder = Dsl.PlainSql();

                string result = builder.SelectAllColumns().From(TableName).Where(inputColumn).In(inputValue).ToSql();

                Assert.Equal($"{DefaulSelectFromWithWhere} {expected}", result);
            }

            [Theory,
             InlineData("ColunaG", new[] { "V1" }, "ColunaG IN ('V1')"),
             InlineData("ColunaH", new[] { "V1", "V2" }, "ColunaH IN ('V1', 'V2')"),
             InlineData("ColunaI", new[] { "V1", "V2", "V3" }, "ColunaI IN ('V1', 'V2', 'V3')")
            ]
            public void ProduceValidSqlQuery_With_NumericInWhereClause(string inputColumn, IEnumerable<string> inputValue, string expected)
            {
                ICanCallAfterSqlBuilder builder = Dsl.PlainSql();

                string result = builder.SelectAllColumns().From(TableName).Where(inputColumn).In(inputValue).ToSql();

                Assert.Equal($"{DefaulSelectFromWithWhere} {expected}", result);
            }

            [Theory,
             InlineData("ColunaJ", "J1", "ColunaK", "K1", "ColunaJ = 'J1' AND ColunaK = 'K1'")
            ]
            public void ProduceValidSqlQuery_With_AndIsEqualClause(string whereColumn, string whereValue, string andColumn, string andValue, string expected)
            {
                ICanCallAfterSqlBuilder builder = Dsl.PlainSql();

                string result = builder.SelectAllColumns().From(TableName).Where(whereColumn).IsEqualTo(whereValue).And(andColumn).IsEqualTo(andValue).ToSql();

                Assert.Equal($"{DefaulSelectFromWithWhere} {expected}", result);
            }
        }
    }
}
