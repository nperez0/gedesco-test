using Core.Events;
using System;

namespace Core.EF.Extensions
{
    internal static class IEventExtensions
    {
        internal static EventEntry ToEventEntry<T>(this T @event, Guid eventId, Guid aggregateId, int version) where T : IEvent
            => new EventEntry(
                eventId,
                aggregateId,
                @event,
                version);
    }
}
