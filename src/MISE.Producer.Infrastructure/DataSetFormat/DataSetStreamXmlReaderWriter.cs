using System.Data;
using System.IO;
using Ardalis.GuardClauses;
using MISE.Producer.Core.Abstractions;

namespace MISE.Producer.Infrastructure.DataSetFormat
{
    public class DataSetStreamXmlReaderWriter : IDataSetStreamReader, IDataSetStreamWriter
    {
        private readonly IAppLogger<DataSetStreamXmlReaderWriter> _logger;

        public DataSetStreamXmlReaderWriter(IAppLogger<DataSetStreamXmlReaderWriter> logger)
        {
            Guard.Against.Null(logger, nameof(logger));
            _logger = logger;
        }
       
        public void Write<T>(T dataSet, Stream stream) where T : DataSet
        {
            Guard.Against.Null(dataSet, nameof(dataSet));
            Guard.Against.Null(stream, nameof(stream));

            _logger.LogDebug("A escrever XML na stream");
            dataSet.WriteXml(stream);
            _logger.LogDebug("XML escrito na stream");
        }

        public T Read<T>(Stream stream) where T : DataSet, new()
        {
            Guard.Against.Null(stream, nameof(stream));
            T dataSet = new T();
            dataSet.ReadXml(stream);
            return dataSet;
        }
    }
}
