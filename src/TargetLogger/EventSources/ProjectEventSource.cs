using JetBrains.Annotations;
using Microsoft.Build.Framework;
using TargetLogger.Logging;

namespace TargetLogger.EventSources
{
    internal sealed class ProjectEventSource : PersistedEventSource
    {
        public ProjectEventSource([NotNull] IContextLogger logger) : base(logger)
        {
        }

        public void OnStarted([NotNull] ProjectStartedEventArgs e)
        {
            Logger.Track(e.BuildEventContext, $"{e.ProjectFile.GetPathFileName()}");
            Logger.Indent(e.BuildEventContext);
            Save(e);
        }

        public void OnFinished([NotNull] ProjectFinishedEventArgs e)
        {
            Logger.Outdent(e.BuildEventContext);
            Logger.Finalize(e.BuildEventContext, GetDuration(e), e.Succeeded);
        }
    }
}