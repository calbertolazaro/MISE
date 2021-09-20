using System.Data;
using System.IO;
using System.IO.Compression;
using Ardalis.GuardClauses;
using MISE.Producer.Core.Abstractions;

namespace MISE.Producer.Infrastructure.DataSetFormat
{
    public class DataSetGZipStreamWriter : IDataSetStreamWriter
    {
        private readonly IAppLogger<DataSetGZipStreamReader> _logger;
        private readonly IDataSetStreamWriter _dateSetStream;

        public DataSetGZipStreamWriter(IAppLogger<DataSetGZipStreamReader> logger,
            IDataSetStreamWriter dateSetStream)
        {
            Guard.Against.Null(logger, nameof(logger));
            Guard.Against.Null(dateSetStream, nameof(dateSetStream));
            _logger = logger;
            _dateSetStream = dateSetStream;
        }
        public void Write(DataSet dataSet, Stream stream)
        {
            Guard.Against.Null(dataSet, nameof(dataSet));
            Guard.Against.Null(stream, nameof(stream));

            _logger.LogDebug("A compactar dados...");

            using (GZipStream gZipstream = new GZipStream(stream,
                CompressionMode.Compress))
            {
                _dateSetStream.Write(dataSet, gZipstream);
            }
            _logger.LogDebug("Os dados foram compactados.");
        }

        public void Write<T>(T dataSet, Stream stream) where T : DataSet
        {
            Guard.Against.Null(dataSet, nameof(dataSet));
            Guard.Against.Null(stream, nameof(stream));

            _logger.LogDebug("A compactar dados...");

            using (GZipStream gZipstream = new GZipStream(stream,
                CompressionMode.Compress))
            {
                _dateSetStream.Write(dataSet, gZipstream);
            }
            _logger.LogDebug("Os dados foram compactados.");
        }
    }
}