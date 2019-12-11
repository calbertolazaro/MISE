namespace MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.SqlClientWrapper
{
    public enum Predicate
    {
        And
    }

    public enum ComparisonMethod
    {
        EqualTo,
        NotEqualTo,
        In
    }

    public class WhereCondition
    {
        public string ColumnName { get; private set; }
        public ComparisonMethod Comparator { get; private set; }
        public object Value { get; private set; }
        public Predicate Predicate { get; private set; }

        public WhereCondition(string columnName, ComparisonMethod comparisonMethod, object value, Predicate predicate)
        {
            ColumnName = columnName;
            Comparator = comparisonMethod;
            Value = value;
            Predicate = predicate;
        }
    }
}
