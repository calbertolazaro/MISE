using System.Data;
using System.IO;
using System.Text.Json.Serialization;
using Ardalis.GuardClauses;
using MISE.Producer.Core.Abstractions;
using Newtonsoft.Json;

namespace MISE.Producer.Infrastructure.DataSetFormat
{
    class DataSetStreamJsonReaderWriter : IDataSetStreamReader, IDataSetStreamWriter
    {
        private readonly IAppLogger<DataSetStreamJsonReaderWriter> _logger;

        public DataSetStreamJsonReaderWriter(IAppLogger<DataSetStreamJsonReaderWriter> logger)
        {
            Guard.Against.Null(logger, nameof(logger));
            _logger = logger;
        }

        public void Write<T>(T dataSet, Stream stream) where T: DataSet
        {
            Guard.Against.Null(dataSet, nameof(dataSet));
            Guard.Against.Null(stream, nameof(stream));
            
            _logger.LogDebug("A escrever Json na stream");

            using (StreamWriter sw = new StreamWriter(stream)) //, Encoding.UTF8, 1024, true))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(sw, dataSet);
            }

            _logger.LogDebug("Json escrito na stream");

        }

        public T Read<T>(Stream stream) where T : DataSet, new()
        {
            Guard.Against.Null(stream, nameof(stream));

            _logger.LogDebug("A ler formato Json...");

            T ds = null;

            using (StreamReader reader = new StreamReader(stream)) //, Encoding.UTF8, 1024, true))
            {
                ds = JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
            }

            return ds;
        }
    }
}
