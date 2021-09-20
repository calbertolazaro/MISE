using System;
using System.Threading;
using MISE.Producer.ConsoleUI.Extensions;

namespace MISE.Producer.UI.Console.Animations
{
    internal static class Animation
    {
        public static CancellationTokenSource Spinner(ConsoleColor color)
        {
            ConsoleSpinner spinner = new ConsoleSpinner();
            return spinner.Spin(color);
        }

    }
}
