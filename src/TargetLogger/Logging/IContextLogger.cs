using JetBrains.Annotations;
using Microsoft.Build.Framework;

namespace TargetLogger.Logging
{
    internal interface IContextLogger
    {
        LoggerVerbosity Verbosity { get; }
        void Warn([NotNull] string message);
        void Error([NotNull] string message);
        void Track([NotNull] BuildEventContext context, [NotNull] string message);
        void Update([NotNull] BuildEventContext context);
        void Finalize([NotNull] BuildEventContext context, bool succeeded);
    }
}