using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.Build.Framework;
using TargetLogger.Logging;

namespace TargetLogger.EventSources
{
    internal class PersistedEventSource : BasicEventSource
    {
        [NotNull] private readonly IDictionary<BuildEventContext, BuildStatusEventArgs> eventContexts;

        protected PersistedEventSource([NotNull] IContextLogger logger) : base(logger)
        {
            eventContexts = new Dictionary<BuildEventContext, BuildStatusEventArgs>();
        }

        protected void Save([NotNull] BuildStatusEventArgs e)
        {
            eventContexts.Add(e.BuildEventContext, e);
        }

        protected TimeSpan GetDuration([NotNull] BuildStatusEventArgs e)
        {
            return eventContexts.TryGetValue(e.BuildEventContext, out var startingEvent)
                ? e.Timestamp.Subtract(startingEvent.Timestamp)
                : TimeSpan.Zero;
        }
    }
}