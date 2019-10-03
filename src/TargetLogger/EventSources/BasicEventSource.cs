namespace TargetLogger.EventSources
{
    internal class BasicEventSource
    {
        protected readonly IContextLogger logger;

        protected BasicEventSource(IContextLogger logger)
        {
            this.logger = logger;
        }
    }
}