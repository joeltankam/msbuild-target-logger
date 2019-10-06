using System;
using JetBrains.Annotations;

namespace TargetLogger
{
    internal static class ConsoleHelper
    {
        public static void Write([NotNull] string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        public static void WriteLine([NotNull] string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}