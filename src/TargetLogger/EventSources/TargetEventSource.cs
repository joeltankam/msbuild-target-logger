using JetBrains.Annotations;
using Microsoft.Build.Framework;
using TargetLogger.Logging;

namespace TargetLogger.EventSources
{
    internal sealed class TargetEventSource : BasicEventSource
    {
        public TargetEventSource([NotNull] IContextLogger logger) : base(logger)
        {
        }

        public void OnStarted([NotNull] TargetStartedEventArgs e)
        {
            Logger.Track(e.BuildEventContext, $"{e.TargetName}");
            Logger.Indent(e.BuildEventContext);
        }

        public void OnFinished([NotNull] TargetFinishedEventArgs e)
        {
            Logger.Outdent(e.BuildEventContext);
            Logger.Finalize(e.BuildEventContext, e.Succeeded);
        }
    }
}