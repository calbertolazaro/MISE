using System;

namespace MISE.Producer.UI.Console
{
    class Program
    {
        /// <summary>
        /// Ponto de entrada de uma aplicação do tipo linha-de-comando
        /// </summary>
        /// <param name="args">Argumentos</param>
        public static int Main(string[] args)
        {
            try
            {
                ConsoleHost.Initialize(args).Run(args);
                return 0;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                return 1;
            }
        }
    }
}
