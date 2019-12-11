using System;
using System.IO;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MISE.Producer.Core;
using MISE.Producer.Core.Abstractions;
using MISE.Producer.Core.RelationalDatabases;
using MISE.Producer.Core.RelationalDatabases.Abstractions;
using MISE.Producer.Core.RelationalDatabases.Schema;
using MISE.Producer.Core.Utils;
using MISE.Producer.Infrastructure.DataSetFormat;
using MISE.Producer.UI.Console.Animations;
using MISE.Producer.UI.Console.Extensions.SysCommand;
using Serilog;
using SysCommand.Mapping;

namespace MISE.Producer.UI.Console.Commands
{
    internal class RelationalDatabaseCommand : DependencyInjectionCommand<RelationalDatabaseCommand>
    {
        public RelationalDatabaseCommand()
        {
            HelpText = "Acções para consultar metadata das Bases de Dados Relacionais";
        }

        [Action(Help = "Mostra todas as bases de dados do servidor")]
        public void SqlCatalogs(
            [Argument(Help = "Identificação da fonte de dados")]
            string dataSource,
            [Argument(Help ="String de ligação ao servidor de SQL, e.g. 'Server=SqlServerTestMachine;Trusted_Connection=True;'")]
            string connectionString)
        {
            // Cria animação de espera em back-ground, obtendo um token para cancelar a animação logo que os dados cheguem.
            CancellationTokenSource animation = Animation.Spinner(ConsoleColor.Yellow);

            try
            {
                App.Console.WriteWithColor("A enviar solicitação ao servidor...", false, ConsoleColor.Yellow);
                
                var producerFacade = ServiceProvider.GetService<IRelationalDatabasesProducerServiceAsync>();

                // Fica à espera que o back-end responda.
                var task = producerFacade.GetCatalogsAsync(dataSource, connectionString);
                task.Wait();
                
                // Os dados já chegaram, pára a animação e mostra os resultados.
                animation.Cancel();

                App.Console.WriteWithColor("A receber resposta do servidor.", false, ConsoleColor.Yellow);

                string output = TableViewExtensions.ToTableView(task.Result.Catalog).Build().ToString();
                App.Console.Write(output, true);

            }
            catch (Exception)
            {
                if (!animation.IsCancellationRequested)
                    animation.Cancel();
            }
        }

        [Action(Help = "Mostra uma base de dados em particular")]
        public void SqlCatalog(
            [Argument(Help = "identificação da fonte de dados")]
            string dataSource,
            [Argument(Help = "String de ligação ao servidor de SQL, e.g. 'Server=SqlServerTestMachine;Trusted_Connection=True;'")]
            string connectionString,
            [Argument(Help = "Identificador da base de dados")]
            string catalogName)
        {
            try
            {
                App.Console.BreakLineInNextWrite = false;
                App.Console.WriteWithColor("A enviar solicitação ao servidor...", false, ConsoleColor.Yellow);

                var producerFacade = ServiceProvider.GetService<IRelationalDatabasesProducerService>();
               
                RelationalDatabaseDataSet results = producerFacade.GetCatalogs(dataSource, connectionString,
                    new[] {new CatalogName(catalogName)});

                App.Console.WriteWithColor("A receber resposta do servidor.", false, ConsoleColor.Yellow);
                string output = TableViewExtensions.ToTableView(results.Catalog).Build().ToString();
                App.Console.Write(output, true);

            }
            catch
            {
                // ignored
            }
        }

        [Action(Help = "Mostra todas as tabelas das bases de dados")]
        public void SqlTables(
            [Argument(Help = "Identificação da fonte de dados")] 
            string dataSource,
            [Argument(Help = "String de ligação ao servidor de SQL, e.g. 'Server=SqlServerTestMachine;Trusted_Connection=True;'")] 
            string connectionString,
            [Argument(Help = "Identificador da base de dados")] 
            string catalogName, 
            [Argument(Help = "Identificador(es) da(s) tabela(s)", IsRequired = false)] 
            string[] tableNameCollection = null)
        {
            try
            {
                App.Console.WriteWithColor("A enviar solicitação ao servidor...", false, ConsoleColor.Yellow);

                var producerFacade = ServiceProvider.GetService<IRelationalDatabasesProducerService>();

                RelationalDatabaseDataSet results =
                    tableNameCollection != null
                        ? producerFacade.GetTables(dataSource, connectionString,
                            CatalogName.FromArray(new[] {catalogName}), TableName.FromArray(tableNameCollection))
                        : producerFacade.GetTables(dataSource, connectionString,
                            CatalogName.FromArray(new[] {catalogName}));
                
                App.Console.WriteWithColor("A receber resposta do servidor.", false, ConsoleColor.Yellow);

                string output = TableViewExtensions.ToTableView(results.Table).Build().ToString();
                App.Console.Write(output, true);
            }
            catch
            {
                // ignored
            }
        }

        [Action(Help = "Cria ficheiro de dados")]
        public void SqlSchema(
            [Argument(Help = "Identificação da fonte de dados")] 
            string dataSource,
            [Argument(Help = "String de ligação ao servidor de SQL, e.g. 'Server=SqlServerTestMachine;Trusted_Connection=True;'")] 
            string connectionString,
            [Argument(Help = "Nome da base de dados")] 
            string[] catalogNameCollection,
            [Argument(Help = "Formato do ficheiro (Xml ou Json)'", IsRequired = false)]
            DataSetStreamFormat fileFormat = DataSetStreamFormat.Xml
        )
        {
            // Cria animação de espera em back-ground, obtendo um token para cancelar a animação logo que os dados cheguem.
            CancellationTokenSource animation = Animation.Spinner(ConsoleColor.Yellow);

            try
            {
                App.Console.WriteWithColor("A enviar solicitação ao servidor...", false, ConsoleColor.Yellow);

                var producerFacade = ServiceProvider.GetService<IRelationalDatabasesProducerServiceAsync>();

                var task = producerFacade.GetSchemaAsync(dataSource, connectionString,
                    CatalogName.FromArray(catalogNameCollection));

                task.Wait();

                // Os dados já chegaram, pára a animação e mostra os resultados.
                animation.Cancel();

                App.Console.WriteWithColor("A receber resposta do servidor.", false, ConsoleColor.Yellow);

                RelationalDatabaseDataSet dataSet = task.Result;

                // Cria nome do ficheiro
                IDataSetStreamWriter dataSetWriter =
                    ServiceProvider.GetRequiredService<DataSetStreamFormatWriterFunc>()(fileFormat);

                var sqlSchemaFileTemplate =
                    ServiceProvider.GetRequiredService<IOptions<ProducerOptions>>().Value.SqlSchemaFileTemplate;

                var fileNameBuilder = new SchemaFileNameBuilder(sqlSchemaFileTemplate,
                    BridgeElement.Category, fileFormat.ToString().ToLower());

                string filePath = fileNameBuilder.Build();
                
                using (FileStream fs = File.Create(filePath))
                {
                    dataSetWriter.Write(dataSet, fs);
                    fs.Close();
                }

                string output =
                    $"O ficheiro '{filePath}' foi criado com {dataSet.Catalog.Count} catálogo(s) e {dataSet.Table.Count} tabela(s).";

                Logger.LogInformation(output);
            }
            catch (Exception e)
            {
                if (!animation.IsCancellationRequested)
                    animation.Cancel();
                Logger.LogError(e.Message);
            }
        }

        [Action(Help="Informação sobre ficheiro de dados criado")]
        public void SchemaInformation(
            [Argument(Help="Caminho completo do ficheiro")] 
            string filePath)
        {
            try
            {
                string extension = Path.GetExtension(filePath).Remove(0, 1);
                if (!Enum.TryParse(extension, out DataSetStreamFormat format))
                    throw new NotSupportedException($"A extenção do ficheiro {filePath} não é reconhecida");

                IDataSetStreamReader dataSetStreamReader = ServiceProvider.GetService<DataSetStreamFormatReaderFunc>()(format);

                var fileStream = new FileStream(filePath, FileMode.Open);

                var dataSet = dataSetStreamReader.Read<RelationalDatabaseDataSet>(fileStream);

                string output =
                    $"O ficheiro '{filePath}' foi criado com {dataSet.Catalog.Count} catálogo(s) e {dataSet.Table.Count} tabela(s).";

                Logger.LogInformation(output);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }
        }
    }
}
