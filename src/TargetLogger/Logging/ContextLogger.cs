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
            ConsoleHelper.WriteLine($"ERR {message}", ConsoleColor.Red);
        }

        public void Track(int id, string message)
        {
            if (entriesByItemId.TryGetValue(id, out var entry))
            {
                entry.Text = message;
                Write(entry, true);
            }
            else
            {
                entry = CreateEntry(message, ConsoleColor.Cyan);
                entriesByItemId.Add(id, entry);
                Write(entry);
            }
        }

        public void Update(int id)
        {
            if (entriesByItemId.TryGetValue(id, out var entry))
                Write(entry, true);
        }

        public void Warn(string message)
        {
            ConsoleHelper.WriteLine($"WRN {message}", ConsoleColor.Yellow);
        }

        public void Finalize(int id, bool succeeded)
        {
            if (entriesByItemId.TryGetValue(id, out var entry))
            {
                entry.Finalize(succeeded);
                Write(entry, true);
            }
            else
                throw new InvalidOperationException($"Trying to finalize item {id} that has not been tracked");
        }

        public static int GetLogId([NotNull] BuildEventContext context)
        {
            // Same as BuildEventContext.GetHashCode(), except we don't go down to task level
            var hash = 17;
            hash = hash * 31 + context.NodeId;
            hash = hash * 31 + context.EvaluationId;
            hash = hash * 31 + context.TargetId;
            hash = hash * 31 + context.ProjectContextId;
            hash = hash * 31 + context.ProjectInstanceId;

            return hash;
        }

        public LoggerVerbosity Verbosity { get; }

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
            ConsoleHelper.WriteLine(logEntry.Text, logEntry.Color);
            if (restoreCursor)
                Console.SetCursorPosition(previousCursorLeft, previousCursorTop);
        }

        private sealed class ContextLoggerEntry
        {
            [NotNull] private readonly Spinner spinner = new Spinner();
            private string text;

            internal ContextLoggerEntry(int position, [NotNull] string text, ConsoleColor color)
            {
                Position = position;
                Color = color;
                this.text = text;
            }

            internal int Position { get; }

            [NotNull]
            internal string Text
            {
                get => $"{spinner.Next()} {text}";
                set => text = value;
            }

            internal ConsoleColor Color { get; private set; }

            public void Finalize(bool succeeded)
            {
                spinner.Stop(succeeded);
                Color = succeeded ? ConsoleColor.Green : ConsoleColor.Red;
            }

            private sealed class Spinner
            {
                private static readonly string[] Sequence = { "/", "-", "\\", "|", "+", "x" };
                private bool finished;
                private int index;

                public string Next()
                {
                    if (!finished)
                        index = (index + 1) % 4;

                    return Sequence[index];
                }

                public void Stop(bool success = true)
                {
                    index = success ? 4 : 5;
                    finished = true;
                }
            }
        }
    }
}