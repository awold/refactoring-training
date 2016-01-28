using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring
{
    class ConsoleHelper
    {

        public static void WriteError(string message)
        {
            WriteMessage(message, ConsoleColor.Red);
        }

        public static void WriteSuccess(string message)
        {
            WriteMessage(message, ConsoleColor.Green);
        }

        private static void WriteMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine();
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void WriteWarning(string message)
        {
            WriteMessage(message, ConsoleColor.Yellow);
        }

        public static string PromptForValue(string text)
        {
            Console.WriteLine();
            Console.WriteLine(text);
            return Console.ReadLine();
        }
    }
}
