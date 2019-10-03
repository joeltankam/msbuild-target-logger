using JetBrains.Annotations;
using Microsoft.Build.Framework;

namespace TargetLogger.EventSources
{
    internal static class ProjectEventSource
    {
        public static void OnStarted([NotNull] object sender, [NotNull] ProjectStartedEventArgs e)
        {
            ContextLogger.WriteLine($"{e.ProjectFile.GetPathFileName()}");
            ContextLogger.Level++;
        }

        public static void OnFinished([NotNull] object sender, ProjectFinishedEventArgs e)
        {
            ContextLogger.Level--;
        }
    }
}
