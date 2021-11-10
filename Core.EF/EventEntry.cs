using Core.Events;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Core.EF
{
    internal class EventEntry
    {
        public EventEntry() { }

        public EventEntry(
            Guid eventId,
            Guid aggregateId,
            IEvent @event,
            int version)
        {
            EventId = eventId;
            AggregateId = aggregateId;
            EventTypeName = @event.GetType().FullName;
            CreationTime = DateTime.Now;
            Content = JsonConvert.SerializeObject(@event);
            Version = version;
        }

        public Guid EventId { get; }

        public Guid AggregateId { get; }

        public string EventTypeName { get; }

        public string EventTypeShortName => EventTypeName.Split('.')?.Last();

        public DateTime CreationTime { get; }

        public string Content { get; }

        public int Version { get; }

        public IEvent Deserialize()
        {
            var type = Type.GetType(EventTypeName);

            return JsonConvert.DeserializeObject(Content, type) as IEvent;
        }
    }
}
