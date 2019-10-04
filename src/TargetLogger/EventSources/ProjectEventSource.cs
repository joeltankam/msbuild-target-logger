using JetBrains.Annotations;
using Microsoft.Build.Framework;
using TargetLogger.Logging;

namespace TargetLogger.EventSources
{
    internal sealed class ProjectEventSource : BasicEventSource
    {
        public ProjectEventSource(IContextLogger logger) : base(logger)
        {
        }

        public void OnStarted([NotNull] ProjectStartedEventArgs e)
        {
            var logItem = new ContextLoggerItem(e.BuildEventContext.GetHashCode(), $"{e.ProjectFile.GetPathFileName()}");
            logger.Track(logItem);
        }

        public void OnFinished([NotNull] ProjectFinishedEventArgs e)
        {
            var logItem = new ContextLoggerItem(e.BuildEventContext.GetHashCode(), $"{e.ProjectFile.GetPathFileName()} finished");
            logger.Track(logItem);
        }
    }
}