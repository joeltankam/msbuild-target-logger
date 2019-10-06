using JetBrains.Annotations;
using TargetLogger.Logging;

namespace TargetLogger.EventSources
{
    internal class BasicEventSource
    {
        [NotNull] protected readonly IContextLogger Logger;

        protected BasicEventSource([NotNull] IContextLogger logger)
        {
            Logger = logger;
        }
    }
}