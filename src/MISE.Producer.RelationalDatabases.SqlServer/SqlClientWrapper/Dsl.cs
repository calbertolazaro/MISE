using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.SqlClientWrapper.Rules;

// Referências:
// 1) https://martinfowler.com/bliki/FluentInterface.html
// 2) https://scottlilly.com/how-to-create-a-fluent-interface-in-c/
// 3) https://scottlilly.com/fluent-interface-creator/
// 4) http://www.postsharp.net/blog/post/webinar-recording-fluent-interfaces
// 5) Martin Fowler DSL Book: https://www.safaribooksonline.com/library/view/domain-specific-languages/9780132107549/ch11.html
// 6) MSDN Magazine: https://msdn.microsoft.com/en-us/magazine/ee291514.aspx
// Sobre o ADO.NET:
// 7) Writing Generic Data Access Code in ASP.NET 2.0 and ADO.NET 2.0 https://msdn.microsoft.com/en-us/library/ms971499.aspx 

namespace MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.SqlClientWrapper
{
    /// <summary>
    /// API fluente (domain specific language) que encapsula a criação da instrução a enviar ao motor de SQL Server usando o ADO.NET
    /// </summary>
    public class Dsl : 
        ICanCallAfterSqlBuilder,
        ICanCallAfterConnectToDb,
        ICanCallAfterUsingThisConnectionString,
        ICanCallAfterForEachCatalog,
        ICanCallAfterForThisCatalog,
        ICanCallAfterSelectAllColumns,
        ICanCallAfterSelectColumn,
        ICanCallAfterFrom,
        ICanCallAfterWithoutAlias,
        ICanCallAfterWhere,
        ICanCallAfterIsEqualTo,
        ICanCallAfterIsNotEqualTo,
        ICanCallAfterIn,
        ICanCallAfterAnd,
        ICanCallAfterAscOrDesc,
        ICanCallAfterOrdering,
        ICanCallAfterAdapt,
        ICanCallAfterUsingThisMapping

    {
        private readonly DbProviderFactory _dbProviderFactory;
        private string _connectionString = string.Empty;

        private readonly List<string> _listOfCatalogs = new List<string>();
        private readonly List<string> _selectedColumns = new List<string>();
        private readonly List<WhereCondition> _whereConditions = new List<WhereCondition>();
        private readonly List<OrderByCondition> _orderByConditions = new List<OrderByCondition>();
        private string _currentOrderByColumn;
        private bool _addAlias = true;
        private string _currentWhereConditionColumn = string.Empty;
        private Predicate _currentWherePredicate = Predicate.And;
        private readonly WhereBuilderStrategy _whereBuilderStrategy;
        private IWhereBuilder _whereBuilderUsed;
        private DataTableMapping _tableMapping;
        private IReadOnlyCollection<string> SelectedColumns => _selectedColumns.AsReadOnly();
        private string TableName { get; set; }
        private IReadOnlyCollection<string> Catalogs => _listOfCatalogs.AsReadOnly();
        private IReadOnlyCollection<WhereCondition> WhereConditions => _whereConditions.AsReadOnly();
        private IReadOnlyCollection<OrderByCondition> OrderByConditions => _orderByConditions.AsReadOnly();
        public static string DefaultAlias => "DslTable";

        public enum WhereBuilderStrategy
        {
            Plain,
            AdoNet
        }

        // Net.Core 2.1 não suporta o carregamento dinâmico da fábrica
        // https://weblog.west-wind.com/posts/2017/Nov/27/Working-around-the-lack-of-dynamic-DbProviderFactory-loading-in-NET-Core
        //private Dsl(string providerName)
        //{
        //    _dbProviderFactory = DbProviderFactories.GetFactory(providerName);
        //    _whereBuilderStrategy = WhereBuilderStrategy.AdoNet;
        //}

        internal Dsl(DbProviderFactory dbProviderFactory, WhereBuilderStrategy strategy = WhereBuilderStrategy.AdoNet)
        {
            _dbProviderFactory = dbProviderFactory;
            _whereBuilderStrategy = strategy;
        }

        internal Dsl(WhereBuilderStrategy strategy)
        {
            _whereBuilderStrategy = strategy;
        }

        // Início da DSL (instantiating functions)
        public static ICanCallAfterConnectToDb ConnectToDb(DbProviderFactory dbProviderFactory)
        {
            return new Dsl(dbProviderFactory);
        }

        public static ICanCallAfterConnectToDb ConnectToMsSqlServer()
		{
            return ConnectToDb(SqlClientFactory.Instance);
		}

        public static ICanCallAfterSqlBuilder PlainSql(WhereBuilderStrategy strategy = WhereBuilderStrategy.Plain)
	    {
	        return new Dsl(strategy);
	    }

        // Sequência dos métodos (chaining functions)
        public ICanCallAfterUsingThisConnectionString UsingThisConnectionString(string connectionString)
        {
            _connectionString = connectionString;
	        return this;
	    }

        public ICanCallAfterForEachCatalog ForEachCatalog(string[] catalogNames)
        {
            foreach (var catalogName in catalogNames)
                ForThisCatalog(catalogName);
            return this;
        }

        public ICanCallAfterForThisCatalog ForThisCatalog(string catalogName)
        {
            if (_listOfCatalogs.Contains(catalogName))
                throw new ArgumentException($"O catálogo {catalogName} já foi adicionado na acção {nameof(ForThisCatalog)}");
            _listOfCatalogs.Add(catalogName);
            return this;
        }

        public ICanCallAfterSelectAllColumns SelectAllColumns()
		{
		    _selectedColumns.Add("*");
            return this;
		}

	    public ICanCallAfterSelectColumn SelectColumn(string columnName)
	    {
	        if (_selectedColumns.Contains(columnName))
	            throw new ArgumentException($"A coluna {columnName} já foi adicionada na acção {nameof(SelectColumn)}");
	        _selectedColumns.Add(columnName);
	        return this;
	    }

        public ICanCallAfterFrom From(string tableName)
		{
            if (String.IsNullOrEmpty(tableName))
                throw new ArgumentException($"A tabela está incorrectamente definida na acção {nameof(From)}");
		    TableName = tableName;
		    return this;
		}

	    public ICanCallAfterWithoutAlias WithoutAlias()
	    {
	        _addAlias = false;
	        return this;
	    }

		public ICanCallAfterWhere Where(string columnName)
		{
            if (String.IsNullOrEmpty(columnName))
                throw  new ArgumentException($"A coluna está incorrectamente definida na acção {nameof(Where)}");
		    _currentWhereConditionColumn = columnName;
            return this;
		}

		public ICanCallAfterIsEqualTo IsEqualTo<T>(T value)
		{
		    _whereConditions.Add(new WhereCondition(_currentWhereConditionColumn, ComparisonMethod.EqualTo, value, _currentWherePredicate));
            return this;
		}

	    public ICanCallAfterIn In<T>(IEnumerable<T> value)
	    {
	        _whereConditions.Add(new WhereCondition(_currentWhereConditionColumn, ComparisonMethod.In, value, _currentWherePredicate));
            return this;
	    }

        public ICanCallAfterIsNotEqualTo IsNotEqualTo<T>(T value)
		{
		    _whereConditions.Add(new WhereCondition(_currentWhereConditionColumn, ComparisonMethod.NotEqualTo, value, _currentWherePredicate));
            return this;
		}

        public ICanCallAfterAnd And(string columnName)
        {
            if (String.IsNullOrEmpty(columnName))
                throw new ArgumentException($"A coluna está incorrectamente definida na acção {nameof(And)}");
            _currentWhereConditionColumn = columnName;
            _currentWherePredicate = Predicate.And;
            return this;
        }

        public ICanCallAfterOrdering OrderBy(string columnName, OrderByCondition.SortMode orderMode = OrderByCondition.SortMode.Ascending)
	    {
	        _currentOrderByColumn = columnName;
	        _orderByConditions.Add(new OrderByCondition(columnName, orderMode));
            return this;
	    }

        public ICanCallAfterAscOrDesc Asc()
		{
		    foreach (var condition in _orderByConditions)
		    {
		        if (0 == String.Compare(_currentOrderByColumn, condition.ColumnName,
		                StringComparison.OrdinalIgnoreCase))
		            condition.SortByMode(OrderByCondition.SortMode.Ascending);
		    }
		    return this;
		}

		public ICanCallAfterAscOrDesc Desc()
		{
		    foreach (var condition in _orderByConditions)
		    {
		        if (0 == String.Compare(_currentOrderByColumn, condition.ColumnName,
		                StringComparison.OrdinalIgnoreCase))
		            condition.SortByMode(OrderByCondition.SortMode.Descending);
		    }
		    return this;
        }

		public ICanCallAfterOrdering ThenBy(string columnName, OrderByCondition.SortMode orderMode = OrderByCondition.SortMode.Ascending)
		{
		    _currentOrderByColumn = columnName;
		    _orderByConditions.Add(new OrderByCondition(columnName, orderMode));
            return this;
		}

		public ICanCallAfterAdapt Adapt()
		{
		    return this;
		}

		public ICanCallAfterUsingThisMapping UsingThisMapping(DataTableMapping tableMapping)
		{
		    _tableMapping = tableMapping;
		    return this;
		}

        #region Métodos de finalização
        public string ToSql()
		{
		    StringBuilder strBuilder = new StringBuilder();

		    // SELECT <SelectedColumns,>
		    // FROM (<TableName>) AS DslTable
		    // WHERE 1=1 AND (<WhereConditions,>)
		    // ORDER BY <OrderByConditions,>
		    //if (_selectedColumns.Count > 0)
		    {
		        strBuilder.Append(BuildSelectStatment());
		        strBuilder.Append(BuildFromStatment());
		        strBuilder.Append(BuildWhereStatment());
		        strBuilder.Append(BuildOrderByStatment());
		    }
		    
		    return strBuilder.ToString(); 
		}

        public void FillDataset(DataSet dataSet)
	    {
            // Cria os objectos ADO.NET (conexão, comando e adaptador)
	        using (DbConnection dbConn = _dbProviderFactory.CreateConnection())
	        {
	            dbConn.ConnectionString = _connectionString;

                // Cria o comando
	            using (DbCommand dbCommand = _dbProviderFactory.CreateCommand())
	            {
	                dbCommand.Connection = dbConn;
	                dbCommand.CommandType = CommandType.Text;
	                dbCommand.CommandText = ToSql();

	                _whereBuilderUsed?.PrepareCommand(dbCommand);

                    // Create adapter
                    using (DbDataAdapter dbAdapter = _dbProviderFactory.CreateDataAdapter())
	                {
	                    if (_tableMapping != null)
	                    {
	                        dbAdapter.TableMappings.Add(_tableMapping);
	                    }

	                    dbAdapter.SelectCommand = dbCommand;

	                    dbConn.Open();

	                    if (Catalogs.Count > 0)
	                    {
	                        foreach (string catalog in Catalogs)
	                        {
	                            dbConn.ChangeDatabase(catalog);

	                            dbAdapter.Fill(dataSet);
	                        }
	                    }
	                    else
	                    {
	                        dbAdapter.Fill(dataSet);
	                    }

	                    dbConn.Close();
	                }
	            }
	        }
	    }

        #endregion

        private string BuildSelectStatment()
        {
            return "SELECT " + String.Join(", ", SelectedColumns) + " ";
        }

        private string BuildFromStatment()
        {
            return "FROM " + (_addAlias ? $"({TableName}) AS {DefaultAlias}" : TableName);
        }

        private string BuildWhereStatment()
        {
            string strWhere = String.Empty;

            if (WhereConditions.Count > 0)
            {
                StringBuilder strWhereBuilder = new StringBuilder(" WHERE ");
                _whereBuilderUsed = CreateWhereBuilder();
                strWhereBuilder.Append(_whereBuilderUsed.BuildWhere(WhereConditions));
                strWhere = strWhereBuilder.ToString();
            }

            return strWhere;
        }

        private string BuildOrderByStatment()
        {
            string strOrderBy = string.Empty;

            if (OrderByConditions.Count > 0)
            {
                StringBuilder strOrderByBuilder = new StringBuilder(" ORDER BY ");

                foreach (OrderByCondition orderByCondition in OrderByConditions)
                {
                    switch (orderByCondition.Sort)
                    {
                        case OrderByCondition.SortMode.Ascending:
                        {
                            strOrderByBuilder.Append($"{orderByCondition.ColumnName} ASC, ");
                            break;
                        }
                        case OrderByCondition.SortMode.Descending:
                        {
                            strOrderByBuilder.Append($"{orderByCondition.ColumnName} DESC, ");
                            break;
                        }
                    }
                }

                strOrderByBuilder.Remove(strOrderByBuilder.Length - 2, 2);

                strOrderBy = strOrderByBuilder.ToString();
            }

            return strOrderBy;
        }

        private IWhereBuilder CreateWhereBuilder()
	    {
	        // Escolhe o algoritmo para criar a instrução SQL
	        // com base em texto simples (plain), inseguro, ou valores parametrizados.
	        switch (_whereBuilderStrategy)
	        {
	            case WhereBuilderStrategy.Plain:
	                return new PlainTextWhereBuilder();
	        }
	        return new DbCommandWhereBuilder();
        }
    }

    
}
