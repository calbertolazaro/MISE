using System;
using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using MISE.Producer.Core.Abstractions;
using MISE.Producer.Infrastructure.DataSetFormat;

namespace MISE.Producer.Infrastructure.Extensions
{
    public static class DataSetStreamServiceCollectionExtension
    {
        public static void AddDataSetStream(this IServiceCollection serviceCollection)
        {
            // Regista as implementações em Json e XML.
            
            serviceCollection.AddTransient<DataSetStreamJsonReaderWriter>();
            serviceCollection.AddTransient<DataSetStreamXmlReaderWriter>();

            // Função-fábrica para escrever a estrutura de dados para um formato
            serviceCollection.AddTransient<DataSetStreamFormatWriterFunc>(serviceProvider => (format) =>
            {
                IDataSetStreamWriter writer = null;

                switch (format)
                {
                    case DataSetStreamFormat.Json:
                        writer = serviceProvider.GetService<DataSetStreamJsonReaderWriter>();
                        break;
                    case DataSetStreamFormat.Jsonc:
                        writer = new DataSetGZipStreamWriter(
                            serviceProvider.GetService<IAppLogger<DataSetGZipStreamReader>>(),
                            serviceProvider.GetService<DataSetStreamJsonReaderWriter>());
                        break;
                    case DataSetStreamFormat.Xml:
                        writer = serviceProvider.GetService<DataSetStreamXmlReaderWriter>();
                        break;
                    case DataSetStreamFormat.Xmlc:
                        writer = new DataSetGZipStreamWriter(
                            serviceProvider.GetService<IAppLogger<DataSetGZipStreamReader>>(),
                            serviceProvider.GetService<DataSetStreamXmlReaderWriter>());
                        break;
                }

                return writer;
            });

            // Função-fábrica para ler a estrutura de dados de um formato
            serviceCollection.AddTransient<DataSetStreamFormatReaderFunc>(serviceProvider => (format) =>
            {
                IDataSetStreamReader reader = null;

                switch (format)
                {
                    case DataSetStreamFormat.Json:
                        reader = serviceProvider.GetService<DataSetStreamJsonReaderWriter>();
                        break;
                    case DataSetStreamFormat.Jsonc:
                        reader = new DataSetGZipStreamReader(
                            serviceProvider.GetService<IAppLogger<DataSetGZipStreamReader>>(),
                            serviceProvider.GetService<DataSetStreamJsonReaderWriter>());
                        break;
                    case DataSetStreamFormat.Xml:
                        reader = serviceProvider.GetService<DataSetStreamXmlReaderWriter>();
                        break;
                    case DataSetStreamFormat.Xmlc:
                        reader = new DataSetGZipStreamReader(
                            serviceProvider.GetService<IAppLogger<DataSetGZipStreamReader>>(),
                            serviceProvider.GetService<DataSetStreamXmlReaderWriter>());
                        break;
                }

                return reader ?? throw new NotSupportedException($"Extensão {format.ToString()} de ficheiro irreconhível");
            });
        }
    }
}
