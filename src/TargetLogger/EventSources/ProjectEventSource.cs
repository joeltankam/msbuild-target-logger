using JetBrains.Annotations;
using Microsoft.Build.Framework;

namespace TargetLogger.EventSources
{
    internal sealed class ProjectEventSource : BasicEventSource
    {
        public ProjectEventSource(IContextLogger logger) : base(logger)
        {
        }

        public void OnStarted([NotNull] ProjectStartedEventArgs e)
        {
            logger.WriteLine($"{e.ProjectFile.GetPathFileName()}");
            logger.Level++;
        }

        public void OnFinished([NotNull] ProjectFinishedEventArgs e)
        {
            logger.Level--;
        }
    }
}