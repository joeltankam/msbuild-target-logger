using JetBrains.Annotations;
using Microsoft.Build.Framework;
using TargetLogger.Logging;

namespace TargetLogger.EventSources
{
    internal sealed class BuildEventSource : BasicEventSource
    {
        public BuildEventSource([NotNull] IContextLogger logger) : base(logger)
        {
        }

        public void OnMessageRaised([NotNull] BuildMessageEventArgs e)
        {
            Logger.Update(e.BuildEventContext);
        }

        public void OnWarningRaised([NotNull] BuildWarningEventArgs e)
        {
            Logger.Warn(e.BuildEventContext, $"{e.Message} @ {e.File}({e.LineNumber},{e.ColumnNumber})");
            Logger.Update(e.BuildEventContext);
        }

        public void OnErrorRaised([NotNull] BuildErrorEventArgs e)
        {
            Logger.Error(e.BuildEventContext, $"{e.Message} @ {e.File}({e.LineNumber},{e.ColumnNumber})");
            Logger.Update(e.BuildEventContext);
        }
    }
}