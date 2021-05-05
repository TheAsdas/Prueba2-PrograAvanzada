using System;

namespace RandomUtils
{
    public class ConsoleUtils
    {
        public static ConsoleColor defaultColor = ConsoleColor.White;
        public static ConsoleColor errorColor = ConsoleColor.Red;
        public static ConsoleColor inputTextColor = ConsoleColor.Yellow;

        /// <summary>
        /// Imprime un error por consola con el estilo determinado.
        /// </summary>
        /// <param name="e">Excepción a imprimir.</param>
        public static void PrintException(Exception e)
        {
            Console.ForegroundColor = errorColor;
            Console.WriteLine("Atención: {0}.", e.Message);
            Console.ResetColor();
        }

        /// <summary>
        /// Escribe una línea en la consola en un color específico, y después retorna al color original.
        /// </summary>
        /// <param name="text">Texto a mostrar por pantalla.</param>
        /// <param name="c">Color del texto.</param>
        public static void WriteLineWithColor(string text, ConsoleColor c)
        {
            Console.ForegroundColor = c;
            Console.WriteLine(text);
            Console.ForegroundColor = defaultColor;
        }

        /// <summary>
        /// Escribe un fragmento de texto en la consola en un color específico, y después retorna al color original.
        /// </summary>
        /// <param name="text">Texto a mostrar por pantalla.</param>
        /// <param name="c">Color del texto.</param>
        public static void WriteWithColor(string text, ConsoleColor c)
        {
            Console.ForegroundColor = c;
            Console.Write(text);
            Console.ForegroundColor = defaultColor;
        }

        /// <summary>
        /// Pide al usuario una línea de texto.
        /// </summary>
        /// <param name="infoText">Texto informativo que específica qué se le está pidiendo al usuario.</param>
        /// <returns><c>String</c> con el input del usuario.</returns>
        public static string GetConsoleInput(string infoText = null)
        {
            if (infoText != null) WriteLineWithColor(infoText, inputTextColor);

            Console.Write("> ");
            return Console.ReadLine();
        }
    }
}
