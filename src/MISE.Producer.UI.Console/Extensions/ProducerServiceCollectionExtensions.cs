using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MISE.Producer.Core;
using MISE.Producer.Core.Abstractions;
using MISE.Producer.Infrastructure.Extensions;
using MISE.Producer.Infrastructure.Logging;
using SysCommand.ConsoleApp;

namespace MISE.Producer.UI.Console.Extensions
{
    internal static class ProducerServiceCollectionExtensions
    {
        internal static void AddProducer(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            // Permite a utilização do serviço IOptions<ProducerOptions>
            serviceCollection.Configure<ProducerOptions>(o => configuration.GetSection("Producer").Bind(o));

            // Logging
            serviceCollection.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            // Escritores de datasets para vários formatos
            serviceCollection.AddDataSetStream();

        }

        /// <summary>
        /// Adiciona aplicação de linha de comando
        /// </summary>
        /// <param name="serviceCollection"></param>
        internal static void AddProducerConsoleApplication(this IServiceCollection serviceCollection)
        {
            // Função para criar App
            serviceCollection.AddTransient(CreateAppFunctionFactory);
            // Aplicação de linha de comando
            serviceCollection.AddSingleton<ConsoleApplication>();
        }

        internal static App CreateAppFunctionFactory(IServiceProvider provider)
        {
            var app = new App();
            app.Console.BreakLineInNextWrite = false;
            app.Items.Add(typeof(IServiceProvider), provider);

            app.OnBeforeMemberInvoke += (appResult, memberResult) =>
            {
                IAppLogger<App> logger = provider.GetService<IAppLogger<App>>();
                logger.LogDebug("{UserName} vai executar {MethodName}", Environment.UserName, memberResult.Name);
            };

            app.OnAfterMemberInvoke += (appResult, memberResult) =>
            {
                IAppLogger<App> logger = provider.GetService<IAppLogger<App>>();
                logger.LogDebug("{UserName} executou o comando {MethodName}", Environment.UserName, memberResult.Name);
            };

            app.OnException += (appResult, exception) =>
            {
                IAppLogger<App> logger = provider.GetService<IAppLogger<App>>();
                logger.LogError("{UserName} executou um comando que provocou a excepção {ErrorMessage}", Environment.UserName,
                    exception.Message);
            };

            return app;
        }
    }
}
