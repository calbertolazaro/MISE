using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Serilog;
using MISE.SharedKernel.Extensions.Hosting;
using MISE.MetadataRegistry.CommandLine.Commands;


// O padrão para linha de comando é: DI + Serilog + Settings
namespace MISE.MedataRegistry.UI.Console
{
    [Command(UnrecognizedArgumentHandling = UnrecognizedArgumentHandling.CollectAndContinue)]
    [Subcommand(typeof(RegistryCommand))]
    //[Subcommand(typeof(ProducerCommand))]
    [HelpOption("--help")]
    //[Command(Name = "registry", Description = "An utility to manage metadata registry database.")
    class Program
    {
        /// <summary>
        /// Ponto de entrada de uma aplicação do tipo linha-de-comando
        /// </summary>
        /// <param name="args">Argumentos</param>
        public static Task<int> Main(string[] args)
        {
            var host = CreateHostBuilder(args);
            return host.RunCommandLineApplicationAsync<Program>(args);
        }
        int OnExecute(CommandLineApplication app, IConsole console)
        {
            app.ShowHelp();
            return 1;
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog((context, loggerConfiguration) =>
                {
                    loggerConfiguration
                        .ReadFrom.Configuration(context.Configuration);
                });
    }
}
