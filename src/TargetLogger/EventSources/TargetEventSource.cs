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

            logger.Track(ContextLogger.GetLogId(e.BuildEventContext), $"{e.TargetName}{suffix}");
        }

        public void OnFinished([NotNull] TargetFinishedEventArgs e)
        {
            logger.Finalize(ContextLogger.GetLogId(e.BuildEventContext), e.Succeeded);
        }
    }
}