using MISE.MetadataRegistry.Core.DataCatalogAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MISE.MetadataRegistry.UnitTests.Core.DataCatalogAggregate
{
    public class DataCatalog_Constructor
    {
        private string _title = "title of data catalog";
        private string _description = "some useful description about the data catalog";
        private DataCatalog _dataCatalog = null;

        private DataCatalog CreateCatalog()
        {
            return new DataCatalog(_title, _description);
        }

        [Fact]
        public void InitializeTitleAndDescription()
        {
            _dataCatalog = CreateCatalog();

            Assert.Equal(_title, _dataCatalog.Title);
            Assert.Equal(_description, _dataCatalog.Description);
        }
    }
}
