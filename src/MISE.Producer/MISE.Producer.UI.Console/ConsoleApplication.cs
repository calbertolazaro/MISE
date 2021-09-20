using Ardalis.GuardClauses;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MISE.Producer.Core;
using MISE.Producer.Core.Abstractions;
using SysCommand.ConsoleApp;

namespace MISE.Producer.UI.Console
{
    internal class ConsoleApplication
    {
        private readonly IAppLogger<ConsoleApplication> _logger;
        private readonly IOptions<ProducerOptions> _options;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly App _app;

        public ConsoleApplication(IAppLogger<ConsoleApplication> logger,
            IOptions<ProducerOptions> options,
            IHostEnvironment hostEnvironment,
            App app
        )
        {
            Guard.Against.Null(logger, nameof(logger));
            Guard.Against.Null(options, nameof(options));
            Guard.Against.Null(hostEnvironment, nameof(hostEnvironment));
            Guard.Against.Null(app, nameof(app));
            _logger = logger;
            _options = options;
            _hostEnvironment = hostEnvironment;
            _app = app;
        }

        internal void Run(string[] args)
        {
            _logger.LogInformation(
                $"Os serviços da aplicação foram iniciados com sucesso no ambiente de {_hostEnvironment.EnvironmentName} por {System.Environment.UserName}");

            ProducerOptions options = _options.Value;
            
            if (args.Length == 0)
                _app.Run(string.IsNullOrEmpty(options.DefaultCommand) ? "help" : options.DefaultCommand);
            else
                _app.Run(args);

            _logger.LogInformation(
                $"A encerrar aplicação no ambiente de {_hostEnvironment.EnvironmentName} por {System.Environment.UserName}");
        }
    }
}