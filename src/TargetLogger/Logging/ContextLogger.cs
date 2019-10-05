using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.Build.Framework;

namespace TargetLogger.Logging
{
    internal sealed class ContextLogger : IContextLogger
    {
        private readonly Dictionary<int, Entry> entriesByItemId = new Dictionary<int, Entry>();

        public ContextLogger(LoggerVerbosity verbosity)
        {
            Verbosity = verbosity;
        }

        public void Error(string message)
        {
            var error = CreateEntry($"X ERR {message}", ConsoleColor.Red);
            Write(error);
        }

        public void Warn(string message)
        {
            var warning = CreateEntry($"! WRN {message}", ConsoleColor.Yellow);
            Write(warning);
        }

        public void Track(ContextLoggerItem logItem)
        {
            if (entriesByItemId.TryGetValue(logItem.Id, out var entry))
            {
                entry.Text = logItem.Text;
                Write(entry, true);
            }
            else
            {
                entry = CreateEntry(logItem.Text);
                entriesByItemId.Add(logItem.Id, entry);
                Write(entry);
            }
        }

        public LoggerVerbosity Verbosity { get; }

        [NotNull]
        private static Entry CreateEntry([NotNull] string text, ConsoleColor color = ConsoleColor.Cyan)
        {
            return new Entry(Console.CursorTop, text, color);
        }

        private static void Write([NotNull] Entry entry, bool restoreCursor = false)
        {
            var previousCursorTop = Console.CursorTop;
            var previousCursorLeft = Console.CursorLeft;
            Console.SetCursorPosition(0, entry.Position);
            Console.ForegroundColor = entry.Color;
            Console.WriteLine(entry.Text);
            Console.ResetColor();
            if (restoreCursor)
                Console.SetCursorPosition(previousCursorLeft, previousCursorTop);
        }

        private sealed class Entry
        {
            internal Entry(int position, [NotNull] string text, ConsoleColor color)
            {
                Position = position;
                Text = text;
                Color = color;
            }

            internal int Position { get; }
            [NotNull] internal string Text { get; set; }
            internal ConsoleColor Color { get; }
        }
    }
}