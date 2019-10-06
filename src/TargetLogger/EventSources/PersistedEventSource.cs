using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.Build.Framework;
using TargetLogger.Logging;

namespace TargetLogger.EventSources
{
    internal class PersistedEventSource : BasicEventSource
    {
        [NotNull] protected readonly IDictionary<BuildEventContext, BuildStatusEventArgs> EventContexts;

        protected PersistedEventSource([NotNull] IContextLogger logger) : base(logger)
        {
            EventContexts = new Dictionary<BuildEventContext, BuildStatusEventArgs>();
        }

        protected void Save([NotNull] BuildStatusEventArgs e)
        {
            EventContexts.Add(e.BuildEventContext, e);
        }

        protected TimeSpan GetDuration([NotNull] BuildStatusEventArgs e)
        {
            return EventContexts.TryGetValue(e.BuildEventContext, out var startingEvent)
                ? e.Timestamp.Subtract(startingEvent.Timestamp)
                : TimeSpan.Zero;
        }
    }
}