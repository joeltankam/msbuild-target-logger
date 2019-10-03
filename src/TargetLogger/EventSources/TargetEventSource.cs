using System;
using JetBrains.Annotations;
using Microsoft.Build.Framework;

namespace TargetLogger.EventSources
{
    internal static class TargetEventSource
    {
        public static void OnStarted([NotNull] object sender, [NotNull] TargetStartedEventArgs e)
        {
            var prefix = ContextLogger.Verbosity >= LoggerVerbosity.Normal ? $"{e.TargetFile.GetPathFileName()}@" : string.Empty;
            ContextLogger.WriteLine($"{prefix}{e.TargetName} started");
        }

        public static void OnFinished([NotNull] object sender, [NotNull] TargetFinishedEventArgs e)
        {
            var prefix = ContextLogger.Verbosity >= LoggerVerbosity.Normal ? $"{e.TargetFile.GetPathFileName()}@" : string.Empty;
            ContextLogger.WriteLine($"{prefix}{e.TargetName} finished", ConsoleColor.Green);
        }
    }
}
