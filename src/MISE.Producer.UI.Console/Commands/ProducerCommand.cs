using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using MISE.Producer.Core;
using MISE.Producer.Core.RelationalDatabases.Abstractions;
using SysCommand.ConsoleApp.View;
using SysCommand.Mapping;

namespace MISE.Producer.UI.Console.Commands
{
    public class ProducerCommand : DependencyInjectionCommand<ProducerCommand>
    {
        public ProducerCommand()
        {
            HelpText = "Mostra informação sobre os produtores de dados do sistema";
        }

        [Action(Help = "Mostra as fontes de ligação a base de dados relacionais")]
        public void DataSources()
        {
            try
            {
                IEnumerable<BridgeMetadata> metadata = ServiceProvider.GetService<IRelationalDatabaseGatewayAssemblyMetadata>().GetRuntimeMetadata();

                string output = TableView
                    .ToTableView(metadata.Select(v => new {v.Category, v.Title, v.Description, v.Creator, v.Publisher}).ToList())
                    .Build().ToString();

                App.Console.Write(output, true);
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
            }
        }
    }
}