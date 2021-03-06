﻿using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.Build.Framework;

namespace TargetLogger.Logging
{
    internal sealed class ContextLogger : IContextLogger
    {
        [NotNull] private readonly Dictionary<int, ContextLoggerEntry> entriesByItemId = new Dictionary<int, ContextLoggerEntry>();
        [NotNull] private readonly Dictionary<int, int> nodeLevels = new Dictionary<int, int>();

        public void Warn(BuildEventContext context, string message)
        {
            var level = GetLevel(context);
            ConsoleHelper.WriteLine($"{string.Empty.PadLeft(level, '\t')}WRN {message}", ConsoleColor.Yellow);
        }

        public void Error(BuildEventContext context, string message)
        {
            var level = GetLevel(context);
            ConsoleHelper.WriteLine($"{string.Empty.PadLeft(level, '\t')}ERR {message}", ConsoleColor.Red);
        }

        public void Track(BuildEventContext context, string message)
        {
            var id = GetId(context);
            if (entriesByItemId.TryGetValue(id, out var entry))
            {
                entry.Text = message;
                Log(entry);
            }
            else
            {
                entry = new ContextLoggerEntry(Console.CursorTop, GetLevel(context), message, ConsoleColor.Cyan);
                entriesByItemId.Add(id, entry);
                Log(entry, false);
            }
        }

        public void Update(BuildEventContext context)
        {
            var id = GetId(context);
            if (entriesByItemId.TryGetValue(id, out var entry))
                Log(entry);
        }

        public void Finalize(BuildEventContext context, TimeSpan duration, bool succeeded)
        {
            var id = GetId(context);
            if (!entriesByItemId.TryGetValue(id, out var entry)) return;

            entry.Finalize(duration, succeeded);
            Log(entry);
        }

        public void Indent(BuildEventContext context)
        {
            var nodeId = context.NodeId;
            if (nodeLevels.ContainsKey(nodeId))
                nodeLevels[nodeId]++;
            else
                nodeLevels.Add(nodeId, 1);
        }

        public void Outdent(BuildEventContext context)
        {
            var nodeId = context.NodeId;
            if (nodeLevels.ContainsKey(nodeId))
                nodeLevels[nodeId]--;
        }

        private int GetLevel([NotNull] BuildEventContext context)
        {
            var level = 0;
            if (nodeLevels.ContainsKey(context.NodeId))
                level = nodeLevels[context.NodeId];

            return level;
        }

        private static int GetId([NotNull] BuildEventContext context)
        {
            // Same as BuildEventContext.GetHashCode(), except we don't go down to task level
            var hash = 17;
            hash = hash * 31 + context.NodeId;
            hash = hash * 31 + context.TargetId;
            hash = hash * 31 + context.ProjectContextId;
            hash = hash * 31 + context.ProjectInstanceId;

            return hash;
        }

        private static void Log([NotNull] ContextLoggerEntry logEntry, bool restoreCursor = true)
        {
            var previousCursorTop = Console.CursorTop;
            var previousCursorLeft = Console.CursorLeft;
            Console.SetCursorPosition(0, logEntry.Position);
            ConsoleHelper.Write(logEntry.Text, logEntry.Color);
            if (logEntry.AdditionalInformation != null)
                ConsoleHelper.Write($" [{logEntry.AdditionalInformation}]", ConsoleColor.DarkGray);

            Console.WriteLine();
            if (restoreCursor)
                Console.SetCursorPosition(previousCursorLeft, previousCursorTop);
        }

        private sealed class ContextLoggerEntry
        {
            private readonly int level;
            [NotNull] private readonly Spinner spinner = new Spinner();
            [NotNull] private string text;

            internal ContextLoggerEntry(int position, int level, [NotNull] string text, ConsoleColor color)
            {
                Position = position;
                Color = color;
                this.level = level;
                this.text = text;
            }

            internal int Position { get; }
            [CanBeNull] internal string AdditionalInformation { get; private set; }

            [NotNull]
            internal string Text
            {
                get => $"{string.Empty.PadLeft(level, '\t')}{spinner.Next()} {text}";
                set => text = value;
            }

            internal ConsoleColor Color { get; private set; }

            public void Finalize(TimeSpan duration, bool succeeded)
            {
                spinner.Stop(succeeded);
                Color = succeeded ? ConsoleColor.Green : ConsoleColor.Red;
                AdditionalInformation = duration.ToShortString();
            }

            private sealed class Spinner
            {
                [NotNull, ItemNotNull] private static readonly string[] Sequence = { "/", "-", "\\", "|", "+", "x" };
                private bool finished;
                private int index;

                [NotNull]
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