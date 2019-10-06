using JetBrains.Annotations;
using Microsoft.Build.Framework;
using TargetLogger.Logging;

namespace TargetLogger.EventSources
{
    internal sealed class BuildEventSource : BasicEventSource
    {
        public BuildEventSource(IContextLogger logger) : base(logger)
        {
        }

        public void OnMessageRaised([NotNull] BuildMessageEventArgs e)
        {
            logger.Update(e.BuildEventContext);
        }

        public void OnWarningRaised([NotNull] BuildWarningEventArgs e)
        {
            logger.Warn(e.BuildEventContext, $"{e.Message} @ {e.File}({e.LineNumber},{e.ColumnNumber})");
            logger.Update(e.BuildEventContext);
        }

        public void OnErrorRaised([NotNull] BuildErrorEventArgs e)
        {
            logger.Error(e.BuildEventContext, $"{e.Message} @ {e.File}({e.LineNumber},{e.ColumnNumber})");
            logger.Update(e.BuildEventContext);
        }
    }
}