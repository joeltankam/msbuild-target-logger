using System.IO;
using JetBrains.Annotations;

namespace TargetLogger
{
    public static class StringExtensions
    {
        [NotNull]
        public static string GetPathFileName([NotNull] this string path)
        {
            return Path.GetFileName(path);
        }
    }
}