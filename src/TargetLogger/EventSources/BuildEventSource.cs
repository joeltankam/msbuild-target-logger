using System;
using JetBrains.Annotations;
using Microsoft.Build.Framework;

namespace TargetLogger.EventSources
{
    internal static class BuildEventSource
    {
        public static void OnErrorRaised([NotNull] object sender, [NotNull] BuildErrorEventArgs e)
        {
            ContextLogger.WriteLine($"X ERR {e.Message} @ {e.File}({e.LineNumber},{e.ColumnNumber})", ConsoleColor.Red);
        }

        public static void OnWarningRaised([NotNull] object sender, [NotNull] BuildWarningEventArgs e)
        {
            ContextLogger.WriteLine($"! WRN {e.Message} @ {e.File}({e.LineNumber},{e.ColumnNumber})", ConsoleColor.Yellow);
        }
    }
}