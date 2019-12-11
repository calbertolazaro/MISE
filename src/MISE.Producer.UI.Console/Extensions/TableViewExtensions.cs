using System.Data;
using SysCommand.ConsoleApp.View;

namespace MISE.Producer.UI.Console.Extensions.SysCommand
{
    internal static class TableViewExtensions
    {
        /// <summary>
        /// Método de ajuda para criar um table view para o tipo DataTable
        /// </summary>
        /// <param name="t"></param>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static TableView ToTableView(this TableView t, DataTable dataTable)
        {
            foreach (DataColumn c in dataTable.Columns)
                t.AddColumnDefinition(c.ColumnName);

            foreach (DataRow r in dataTable.Rows)
            {
                TableView.Row tvRow = t.AddRow();

                foreach (DataColumn c in dataTable.Columns)
                {
                    object value = r[c];

                    tvRow.AddColumnInRow(value.ToString() ?? string.Empty);
                }
            }

            return t;
        }

        /// <summary>
        /// Método de ajuda para criar um table view para o tipo DataTable (método marcado como estático)
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static TableView ToTableView(DataTable dataTable)
        {
            var table = new TableView();

            table.IncludeHeader = true;

            return table.ToTableView(dataTable);
        }
    }
}
