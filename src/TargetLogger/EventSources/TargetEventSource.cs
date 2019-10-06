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

            logger.Track(e.BuildEventContext, $"{e.TargetName}{suffix}");
            logger.Indent(e.BuildEventContext);
        }

        public void OnFinished([NotNull] TargetFinishedEventArgs e)
        {
            logger.Outdent(e.BuildEventContext);
            logger.Finalize(e.BuildEventContext, e.Succeeded);
        }
    }
}