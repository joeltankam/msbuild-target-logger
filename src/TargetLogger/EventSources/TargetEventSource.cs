using JetBrains.Annotations;
using Microsoft.Build.Framework;
using TargetLogger.Logging;

namespace TargetLogger.EventSources
{
    internal sealed class TargetEventSource : BasicEventSource
    {
        public TargetEventSource(IContextLogger logger) : base(logger)
        {
        }

        public void OnStarted([NotNull] TargetStartedEventArgs e)
        {
            var suffix = logger.Verbosity >= LoggerVerbosity.Normal
                ? $" @{e.TargetFile.GetPathFileName()}"
                : string.Empty;

            var logItem = new ContextLoggerItem(e.BuildEventContext.GetHashCode(), $"{e.TargetName}{suffix}");
            logger.Track(logItem);
        }

        public void OnFinished([NotNull] TargetFinishedEventArgs e)
        {
            var suffix = logger.Verbosity >= LoggerVerbosity.Normal
                ? $" @{e.TargetFile.GetPathFileName()}"
                : string.Empty;

            var status = e.Succeeded ? ContextLoggerItemStatus.Success : ContextLoggerItemStatus.Failure;
            var logItem = new ContextLoggerItem(e.BuildEventContext.GetHashCode(), $"{e.TargetName}{suffix} finished", status);
            logger.Track(logItem);
        }
    }
}