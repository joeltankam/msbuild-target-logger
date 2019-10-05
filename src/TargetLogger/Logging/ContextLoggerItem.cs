using JetBrains.Annotations;

namespace TargetLogger.Logging
{
    internal sealed class ContextLoggerItem
    {
        public ContextLoggerItem(int id, [NotNull] string text, ContextLoggerItemStatus status = ContextLoggerItemStatus.None)
        {
            Id = id;
            Text = text;
            Status = status;
        }

        public int Id { get; }
        [NotNull] public string Text { get; }
        public ContextLoggerItemStatus Status { get; }
    }
}