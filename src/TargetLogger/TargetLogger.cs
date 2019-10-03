using JetBrains.Annotations;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using TargetLogger.EventSources;

namespace TargetLogger
{
    [UsedImplicitly]
    public sealed class TargetLogger : Logger
    {
        private readonly TargetEventSource targetEventSource;
        private readonly BuildEventSource buildEventSource;
        private readonly ProjectEventSource projectEventSource;

        public TargetLogger()
        {
            var logger = new ContextLogger(Verbosity);
            targetEventSource = new TargetEventSource(logger);
            buildEventSource = new BuildEventSource(logger);
            projectEventSource = new ProjectEventSource(logger);
        }

        public override void Initialize([NotNull] IEventSource eventSource)
        {
            eventSource.TargetStarted += (sender, args) => targetEventSource.OnStarted(args);
            eventSource.TargetFinished += (sender, args) => targetEventSource.OnFinished(args);
            eventSource.ErrorRaised += (sender, args) => buildEventSource.OnErrorRaised(args);
            eventSource.WarningRaised += (sender, args) => buildEventSource.OnWarningRaised(args);
            eventSource.ProjectStarted += (sender, args) => projectEventSource.OnStarted(args);
            eventSource.ProjectFinished += (sender, args) => projectEventSource.OnFinished(args);
        }
    }
}