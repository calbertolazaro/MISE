namespace MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.SqlClientWrapper
{
    public class OrderByCondition
    {
        public enum SortMode
        {
            Ascending,
            Descending
        }

        public string ColumnName { get; private set; }
        public SortMode Sort { get; private set; }

        public OrderByCondition(string columnName, SortMode sortMode)
        {
            ColumnName = columnName;
            Sort = sortMode;
        }

        public void SortByMode(SortMode mode)
        {
            Sort = mode;
        }
    }
}
