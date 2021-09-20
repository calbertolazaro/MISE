using System;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using MISE.Producer.Core.Abstractions;
using SysCommand.ConsoleApp;

namespace MISE.Producer.UI.Console.Commands
{
    /// <summary>
    /// Comando com acesso ao componentes de injecção de dependências e de logging
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class DependencyInjectionCommand<T> : Command
    {
        protected IAppLogger<T> Logger => ServiceProvider.GetRequiredService<IAppLogger<T>>();
        protected IServiceProvider ServiceProvider => App.Items.Get<IServiceProvider>();
    }
}
