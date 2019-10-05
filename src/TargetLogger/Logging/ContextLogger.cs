using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.Build.Framework;

namespace TargetLogger.Logging
{
    internal sealed class ContextLogger : IContextLogger
    {
        private readonly Dictionary<int, ContextLoggerEntry> entriesByItemId = new Dictionary<int, ContextLoggerEntry>();

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
                UpdateEntry(entry, logItem);
                Write(entry, true);
            }
            else
            {
                entry = CreateEntry(logItem);
                entriesByItemId.Add(logItem.Id, entry);
                Write(entry);
            }
        }

        public void Finalize(ContextLoggerItem logItem)
        {
            if (entriesByItemId.TryGetValue(logItem.Id, out var entry))
            {
                UpdateEntry(entry, logItem);
                Write(entry, true);
            }
            else
                throw new InvalidOperationException($"Trying to finalize item {logItem.Id} that has not been tracked");
        }

        public LoggerVerbosity Verbosity { get; }

        private static void UpdateEntry([NotNull] ContextLoggerEntry logEntry, [NotNull] ContextLoggerItem logItem)
        {
            logEntry.Text = logItem.Text;
            logEntry.Color = GetEntryColor(logItem);
        }

        private static ConsoleColor GetEntryColor([NotNull] ContextLoggerItem logItem)
        {
            switch (logItem.Status)
            {
                case ContextLoggerItemStatus.None:
                    return ConsoleColor.Cyan;
                case ContextLoggerItemStatus.Success:
                    return ConsoleColor.Green;
                case ContextLoggerItemStatus.Failure:
                    return ConsoleColor.Red;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        [NotNull]
        private static ContextLoggerEntry CreateEntry([NotNull] ContextLoggerItem logItem)
        {
            return CreateEntry(logItem.Text, GetEntryColor(logItem));
        }

        [NotNull]
        private static ContextLoggerEntry CreateEntry([NotNull] string text, ConsoleColor color)
        {
            return new ContextLoggerEntry(Console.CursorTop, text, color);
        }

        private static void Write([NotNull] ContextLoggerEntry logEntry, bool restoreCursor = false)
        {
            var previousCursorTop = Console.CursorTop;
            var previousCursorLeft = Console.CursorLeft;
            Console.SetCursorPosition(0, logEntry.Position);
            Console.ForegroundColor = logEntry.Color;
            Console.WriteLine(logEntry.Text);
            Console.ResetColor();
            if (restoreCursor)
                Console.SetCursorPosition(previousCursorLeft, previousCursorTop);
        }

        private sealed class ContextLoggerEntry
        {
            internal ContextLoggerEntry(int position, [NotNull] string text, ConsoleColor color)
            {
                Position = position;
                Text = text;
                Color = color;
            }

            internal int Position { get; }
            [NotNull] internal string Text { get; set; }
            internal ConsoleColor Color { get; set; }
        }
    }
}