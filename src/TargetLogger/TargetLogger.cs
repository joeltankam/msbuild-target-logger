using JetBrains.Annotations;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using TargetLogger.EventSources;

namespace TargetLogger
{
    public class TargetLogger : Logger
    {
        public override void Initialize([NotNull] IEventSource eventSource)
        {
            ContextLogger.Verbosity = Verbosity;
            eventSource.TargetStarted += TargetEventSource.OnStarted;
            eventSource.TargetFinished += TargetEventSource.OnFinished;
            eventSource.ErrorRaised += BuildEventSource.OnErrorRaised;
            eventSource.WarningRaised += BuildEventSource.OnWarningRaised;
            eventSource.ProjectStarted += ProjectEventSource.OnStarted;
            eventSource.ProjectFinished += ProjectEventSource.OnFinished;
        }
    }
}