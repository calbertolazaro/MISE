using MISE.Producer.Core.Abstractions;

namespace MISE.Producer.Infrastructure.DataSetFormat
{
    public enum DataSetStreamFormat
    {
        Xml,
        Json,
        Xmlc,
        Jsonc
    }

    /// <summary>
    /// Delegate para escolher o escritor do formato de saída quando se extrai os metadados dos sistemas para ficheiro
    /// </summary>
    /// <param name="format">Formato de exportação</param>
    /// <returns></returns>
    public delegate IDataSetStreamWriter DataSetStreamFormatWriterFunc(DataSetStreamFormat format);

    /// <summary>
    /// Delegate para escolher o leitor do formato do ficheiro
    /// </summary>
    /// <param name="format">Formato de exportação</param>
    /// <returns></returns>
    public delegate IDataSetStreamReader DataSetStreamFormatReaderFunc(DataSetStreamFormat format);
}
