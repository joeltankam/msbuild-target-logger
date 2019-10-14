using System;
using System.Text;
using JetBrains.Annotations;

namespace TargetLogger
{
    internal static class TimeSpanExtensions
    {
        [NotNull]
        public static string ToShortString(this TimeSpan duration)
        {
            var value = new StringBuilder();

            if (duration.TotalSeconds < 1)
                value.Append($"{duration.TotalMilliseconds} ms");
            else
            {
                var days = int.Parse(duration.ToString("%d"));
                if (days > 0) value.Append($"{days} d");

                var hours = int.Parse(duration.ToString("%h"));
                if (hours > 0)
                {
                    if (value.Length != 0) value.Append(" ");
                    value.Append($"{hours} h");
                }

                var minutes = int.Parse(duration.ToString("%m"));
                if (minutes > 0)
                {
                    if (value.Length != 0) value.Append(" ");
                    value.Append($"{minutes} min");
                }

                var seconds = int.Parse(duration.ToString("%s"));
                if (seconds > 0)
                {
                    if (value.Length != 0) value.Append(" ");
                    value.Append($"{seconds} sec");
                }
            }

            return value.ToString();
        }
    }
}