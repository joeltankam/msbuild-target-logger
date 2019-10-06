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
            logger.Track(e.BuildEventContext, $"{e.ProjectFile.GetPathFileName()}");
        }

        public void OnFinished([NotNull] ProjectFinishedEventArgs e)
        {
            logger.Finalize(e.BuildEventContext, e.Succeeded);
        }
    }
}