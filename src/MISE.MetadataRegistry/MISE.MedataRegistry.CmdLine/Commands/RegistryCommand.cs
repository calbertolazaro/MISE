using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

namespace MISE.MetadataRegistry.CommandLine.Commands
{
    [Command("registry", Description = "Manage metadata registry database"),
    Subcommand(typeof(Import))]
    internal class RegistryCommand
    {
        int OnExecute(CommandLineApplication app, IConsole console)
        {
            console.Error.WriteLine("You must specify an action. See --help for more details.");
            app.ShowHelp();
            return 1;
        }

        [Command(Description = "Import producer files"), HelpOption]
        private class Import
        {
            [Required]
            [Option("--file", Description = "Required. The producer file to be imported")]
            public string File { get; }

            private void OnExecute(IConsole console)
            {
                console.WriteLine($"File is {File}");
            }
        }

    }

    //[Command("producer", Description = "Run producer"]
    //internal class ProducerCommand
    //{
    //    private int OnExecute(IConsole console)
    //    {
    //        console.Error.WriteLine("You must specify an action. See --help for more details.");
    //        return 1;
    //    }

    //    [Command(Name = "Import", Description = "Import producer file")]
    //    private class Import
    //    {
    //        [Required]
    //        [Option("--file", Description = "Required. The file to be imported")]            
    //        public string File { get; }

    //        private void OnExecute(IConsole console)
    //        {
    //            console.WriteLine($"importing file: {File}");
    //        }
    //    }
    //}
}
