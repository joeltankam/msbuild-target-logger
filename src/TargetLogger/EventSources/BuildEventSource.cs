using System;
using JetBrains.Annotations;
using Microsoft.Build.Framework;

namespace TargetLogger.EventSources
{
    internal sealed class BuildEventSource : BasicEventSource
    {
        public BuildEventSource(IContextLogger logger) : base(logger)
        {
        }

        public void OnErrorRaised([NotNull] BuildErrorEventArgs e)
        {
            logger.WriteLine($"X ERR {e.Message} @ {e.File}({e.LineNumber},{e.ColumnNumber})", ConsoleColor.Red);
        }

        public void OnWarningRaised([NotNull] BuildWarningEventArgs e)
        {
            logger.WriteLine($"! WRN {e.Message} @ {e.File}({e.LineNumber},{e.ColumnNumber})", ConsoleColor.Yellow);
        }
    }
}