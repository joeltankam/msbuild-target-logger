using System;
using JetBrains.Annotations;
using Microsoft.Build.Framework;

namespace TargetLogger
{
    internal interface IContextLogger
    {
        LoggerVerbosity Verbosity { get; }
        int Level { get; set; }
        void WriteLine([NotNull] string message, ConsoleColor color = ConsoleColor.Cyan);
    }

    internal sealed class ContextLogger : IContextLogger
    {
        public ContextLogger(LoggerVerbosity verbosity)
        {
            Verbosity = verbosity;
        }

        public void WriteLine(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(string.Empty.PadLeft(Level, '\t'));
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public LoggerVerbosity Verbosity { get; }
        public int Level { get; set; }
    }
}