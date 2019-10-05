using JetBrains.Annotations;
using Microsoft.Build.Framework;

namespace TargetLogger.Logging
{
    internal interface IContextLogger
    {
        LoggerVerbosity Verbosity { get; }
        void Warn([NotNull] string message);
        void Error([NotNull] string message);
        void Track([NotNull] ContextLoggerItem logItem);
        void Finalize([NotNull] ContextLoggerItem logItem);
    }
}