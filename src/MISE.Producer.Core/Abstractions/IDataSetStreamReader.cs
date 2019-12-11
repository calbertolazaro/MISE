using System.Data;
using System.IO;

namespace MISE.Producer.Core.Abstractions
{
    /// <summary>
    /// Abstração de leitura da estrutura de dados System.Data.DataSet
    /// </summary>
    public interface IDataSetStreamReader
    {
        T Read<T>(Stream stream) where T : DataSet, new();
    }
}