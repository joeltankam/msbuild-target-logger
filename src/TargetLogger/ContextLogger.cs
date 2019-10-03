using System;
using JetBrains.Annotations;
using Microsoft.Build.Framework;

namespace TargetLogger
{
    internal static class ContextLogger
    {
        public static void WriteLine([NotNull] string message, ConsoleColor color = ConsoleColor.Cyan)
        {
            Console.ForegroundColor = color;
            Console.Write(string.Empty.PadLeft(Level, '\t'));
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static LoggerVerbosity Verbosity { get; set; }
        public static int Level { get; set; }
    }
}