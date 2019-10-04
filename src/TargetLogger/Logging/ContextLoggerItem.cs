using JetBrains.Annotations;

namespace TargetLogger.Logging
{
    internal sealed class ContextLoggerItem
    {
        public ContextLoggerItem(int id, [NotNull] string text)
        {
            Id = id;
            Text = text;
        }

        public int Id { get; }
        [NotNull] public string Text { get; }
    }
}