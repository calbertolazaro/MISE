using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MISE.Producer.Extensions.DependencyInjection.RelationalDatabases;
using MISE.Producer.UI.Console.Extensions;
using Serilog;
using Serilog.Events;

namespace MISE.Producer.UI.Console
{
    static class ConsoleHost
    {
        private const string _prefix = "MISE_PRODUCER_";
        private const string _appsettings = "appsettings.json";
        private const string _hostsettings = "hostsettings.json";
        public static ConsoleApplication Initialize(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureHostConfiguration(configHost =>
                {
                    var rootPath = Directory.GetCurrentDirectory();
                    configHost.SetBasePath(rootPath);
                    configHost.AddJsonFile(_hostsettings, true);
                    configHost.AddEnvironmentVariables(_prefix);
                })
                .ConfigureAppConfiguration((hostContext, configApp) =>
                {
                    configApp.SetBasePath(Directory.GetCurrentDirectory());
                    configApp.AddJsonFile(_appsettings, true);
                    configApp.AddJsonFile(
                        $"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json",
                        optional: true);
                    configApp.AddCommandLine(args);
                }).ConfigureServices((hostContext, services) =>
                {
                    // Adiciona serviços do produtor (logging da aplicação e escritores para ficheiros)
                    // e aplicaçao de linha de comando
                    services.AddProducer(hostContext.Configuration);
                    // Serviço do produtor para SGBDR
                    services.AddProducerRelationalDatabases();
                    //TODO:services.AddProducerSqlServerIntegrationServices();
                    //TODO:services.AddProducerSqlServerReportingServices();
                    // Aplicação de linha de comando
                    services.AddProducerConsoleApplication();
                })
                .ConfigureLogging((hostContext, configLogging) =>
                {
                    // Remove pré-instalados
                    configLogging.ClearProviders();
                })
                .UseSerilog((context, loggerConfiguration) =>
                {
                    loggerConfiguration
                        //.MinimumLevel.Information()
                        //.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                        .ReadFrom.Configuration(context.Configuration);
                    //.Enrich.FromLogContext();
                })
                .Build();

            return host.Services.GetRequiredService<ConsoleApplication>();
        }
    }

    // Documentação online sobre um "host"
    // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-3.0

    // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.0
    // https://andrewlock.net/exploring-the-new-project-file-program-and-the-generic-host-in-asp-net-core-3/

    // https://docs.microsoft.com/en-us/dotnet/standard/asynchronous-programming-patterns/
    // https://blog.stephencleary.com/2012/02/async-and-await.html
    // https://www.codingame.com/playgrounds/4240/your-ultimate-async-await-tutorial-in-c/introduction
    // https://devblogs.microsoft.com/premier-developer/dissecting-the-async-methods-in-c/


    // https://michaelscodingspot.com/logging-in-dotnet/?fbclid=IwAR3N5u1xcG_tvbrKJ8PCaoOB4jie4Mu_G3kA1CDgsbcjWAEeD49bTreTF_k

}