using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.Build.Framework;

namespace TargetLogger.Logging
{
    internal sealed class ContextLogger : IContextLogger
    {
        private readonly int cursorOffSetTop;
        private readonly List<Entry> entries = new List<Entry>();
        private readonly Dictionary<int, Entry> itemPositions = new Dictionary<int, Entry>();

        public ContextLogger(LoggerVerbosity verbosity)
        {
            Verbosity = verbosity;
            cursorOffSetTop = Console.CursorTop;
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
            if (itemPositions.TryGetValue(logItem.Id, out var entry))
            {
                entry.Text = logItem.Text;
                Write(entry, true);
            }
            else
            {
                entry = CreateEntry(logItem.Text);
                itemPositions.Add(logItem.Id, entry);
                Write(entry);
            }
        }

        public LoggerVerbosity Verbosity { get; }

        [NotNull]
        private Entry CreateEntry([NotNull] string text, ConsoleColor color = ConsoleColor.Cyan)
        {
            var entry = new Entry(entries.Count, text, color);
            entries.Add(entry);
            return entry;
        }

        private void Write([NotNull] Entry entry, bool restoreCursor = false)
        {
            var previousCursorTop = Console.CursorTop;
            var previousCursorLeft = Console.CursorLeft;
            Console.SetCursorPosition(0, cursorOffSetTop + entry.Position);
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