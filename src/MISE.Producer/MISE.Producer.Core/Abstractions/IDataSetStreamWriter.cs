using System.Data;
using System.IO;

namespace MISE.Producer.Core.Abstractions
{
    public interface IDataSetStreamWriter
    {
        void Write<T>(T dataSet, Stream stream) where T : DataSet;
    }
}