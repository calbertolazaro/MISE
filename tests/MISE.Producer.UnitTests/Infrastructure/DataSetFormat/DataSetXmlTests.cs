using System.Data;
using System.IO;
using MISE.Producer.Core.Abstractions;
using MISE.Producer.Infrastructure.DataSetFormat;
using Moq;
using Xunit;

namespace MISE.Producer.Tests.Infrastructure.DataSetFormat
{
    public class DataSetXmlReaderWriterTests
    {
        private string _dataSetName = "SomeDataSet";

        [Fact]
        public void CanWriteXmlToStream()
        {
            DataSet dataSet = new DataSetBuilder().CreateAndBuildDefaultDbSet(_dataSetName);
            MemoryStream memStream = new MemoryStream();
            var mockLogger = Mock.Of<IAppLogger<DataSetStreamXmlReaderWriter>>();


            IDataSetStreamWriter writer = new DataSetStreamXmlReaderWriter(mockLogger);
            writer.Write(dataSet, memStream);

            Assert.True(memStream.Length > 0);
            memStream.Dispose();
        }

        [Fact]
        public void CanReadXmlFromStream()
        {
            DataSet dataSet = new DataSetBuilder().CreateAndBuildDefaultDbSet(_dataSetName);
            MemoryStream memStream = new MemoryStream();
            var mockLogger = Mock.Of<IAppLogger<DataSetStreamXmlReaderWriter>>();
            
            IDataSetStreamWriter writer = new DataSetStreamXmlReaderWriter(mockLogger);
            writer.Write(dataSet, memStream);
            
            memStream.Seek(0, SeekOrigin.Begin);

            IDataSetStreamReader reader = new DataSetStreamXmlReaderWriter(mockLogger);
            var ds = reader.Read<DataSet>(memStream);

            Assert.Equal(_dataSetName, ds.DataSetName);
            Assert.Equal(DataSetBuilder.DefaultDataSetTablesCount, ds.Tables.Count);
            Assert.Equal(DataSetBuilder.DefaultCatalogRowsCount, dataSet.Tables[DataSetBuilder.CatalogTableName].Rows.Count);
            
            memStream.Dispose();
        }
    }

    public class DataSetBuilder
    {
        public static int DefaultCatalogRowsCount;
        public static int DefaultDataSetTablesCount;

        public static string CatalogTableName = "Catalog";

        public DataSetBuilder()
        {
        }

        public DataSet CreateAndBuildDefaultDbSet(string dataSetName)
        {
            DataSet dataSet = new DataSet(dataSetName);

            DataTable catalog = new DataTable(CatalogTableName);
            catalog.Columns.Add(new DataColumn("Name", typeof(string)));

            catalog.Rows.Add("tempdb");
            catalog.Rows.Add("master");
            catalog.Rows.Add("msdb");

            dataSet.Tables.Add(catalog);

            DefaultCatalogRowsCount = catalog.Rows.Count;
            DefaultDataSetTablesCount = dataSet.Tables.Count;
            return dataSet;
        }

    }
}
