using System.Data;
using System.IO;
using System.IO.Compression;
using Ardalis.GuardClauses;
using MISE.Producer.Core.Abstractions;

namespace MISE.Producer.Infrastructure.DataSetFormat
{
    public class DataSetGZipStreamReader : IDataSetStreamReader
    {
        private readonly IAppLogger<DataSetGZipStreamReader> _logger;
        private readonly IDataSetStreamReader _dateSetStream;
        public DataSetGZipStreamReader(IAppLogger<DataSetGZipStreamReader> logger, IDataSetStreamReader dateSetStream)
        {
            Guard.Against.Null(logger, nameof(logger));
            Guard.Against.Null(dateSetStream, nameof(dateSetStream));
            _logger = logger;
            _dateSetStream = dateSetStream;
        }

        public T Read<T>(Stream stream) where T : DataSet, new()
        {
            Guard.Against.Null(stream, nameof(stream));

            _logger.LogDebug("A descompactar dados...");

            T dataSet = null;

            using (GZipStream gZipstream = new GZipStream(stream,
                CompressionMode.Decompress))

            {
                dataSet =_dateSetStream.Read<T>(gZipstream);
            }

            _logger.LogDebug("Os dados foram descompactados.");

            return dataSet;
        }

        public DataSet Read(Stream stream)
        {
            return Read<DataSet>(stream);
        }
    }
}