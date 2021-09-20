using System;
using System.Threading;
using System.Threading.Tasks;
using MISE.Producer.UI.Console.Animations;

namespace MISE.Producer.ConsoleUI.Extensions
{
    internal static class ConsoleSpinnerExtensions
    {
        public static CancellationTokenSource Spin(this ConsoleSpinner spinner, ConsoleColor color)
        {
            // Cria uma thread em background para executar a animação.
            // Devolve um token ao invocador para este decidir quando deve parar a animação.
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            Task.Run(async () =>
            {
                Console.CursorVisible = false;
                while (!tokenSource.IsCancellationRequested)
                {
                    Console.ForegroundColor = color;
                    spinner.UpdateProgress();
                    Console.ResetColor();
                    await Task.Delay(100);
                }
                Console.CursorVisible = true;
            }, tokenSource.Token);

            return tokenSource;
        }
    }
}