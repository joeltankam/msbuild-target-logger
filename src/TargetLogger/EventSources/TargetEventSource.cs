using System;
using JetBrains.Annotations;
using Microsoft.Build.Framework;

namespace TargetLogger.EventSources
{
    internal sealed class TargetEventSource : BasicEventSource
    {
        public TargetEventSource(IContextLogger logger) : base(logger)
        {
        }

        public void OnStarted([NotNull] TargetStartedEventArgs e)
        {
            var prefix = logger.Verbosity >= LoggerVerbosity.Normal
                ? $"{e.TargetFile.GetPathFileName()}@"
                : string.Empty;

            logger.WriteLine($"{prefix}{e.TargetName} started");
        }

        public void OnFinished([NotNull] TargetFinishedEventArgs e)
        {
            var prefix = logger.Verbosity >= LoggerVerbosity.Normal
                ? $"{e.TargetFile.GetPathFileName()}@"
                : string.Empty;

            logger.WriteLine($"{prefix}{e.TargetName} finished", ConsoleColor.Green);
        }
    }
}