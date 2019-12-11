using System;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using MISE.Producer.UI.Console.Extensions;
using SysCommand.ConsoleApp;
using SysCommand.ConsoleApp.Helpers;

namespace MISE.Producer.UI.Console.Commands
{
    internal class ShellCommand: DependencyInjectionCommand<ShellCommand>
    {
        public ShellCommand()
        {
            HelpText = "Cria shell para execução das acções";

            System.Console.CancelKeyPress += Console_CancelKeyPress;
        }

        public void Shell(string name = "MISE")
        {
            bool flag = false;

            while (true)
            {
                App app = ServiceProvider.GetService<App>();

                app.Console.BreakLineInNextWrite = flag;

                var args = GetArguments(app, name);

                if (args.Length == 1)
                {
                    if (0 == string.CompareOrdinal(args[0], "exit"))
                        break;
                    if (0 == string.CompareOrdinal(args[0], "shell"))
                        break;
                    if (0 == string.CompareOrdinal(args[0], "cls"))
                        System.Console.Clear();
                    else
                    {
                        app.Run(args);
                    }
                }
                else
                {
                    app.Run(args);
                }

                flag = app.Console.BreakLineInNextWrite;
            }

            App.Console.Write((object)"Adeus!\r\n", false, false);
        }

        private static string[] GetArguments(App app, string shellName)
        {
            return ConsoleAppHelper.StringToArgs(app.Console.Read(shellName.TrimEnd() + ">", false));
        }

        private void Console_CancelKeyPress(object sender, System.ConsoleCancelEventArgs e)
        {
            e.Cancel = true;
            Logger.LogInformation("Escreva 'exit' para encerrar a aplicação");
        }
    }
}
