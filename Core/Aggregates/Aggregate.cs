using Core.Events;
using System;
using System.Collections.Generic;

namespace Core.Aggregates
{
    public abstract class Aggregate : Aggregate<Guid>, IAggregate
    {
    }

    public abstract class Aggregate<T> : IAggregate<T> where T : notnull
    {
        public T Id { get; protected set; } = default!;

        public int Version { get; protected set; }

        [NonSerialized] private readonly Queue<IEvent> _uncommittedEvents = new Queue<IEvent>();

        public virtual void When(object @event) { }

        public IEvent[] DequeueUncommittedEvents()
        {
            var dequeuedEvents = _uncommittedEvents.ToArray();

            _uncommittedEvents.Clear();

            return dequeuedEvents;
        }

        protected void Enqueue(IEvent @event)
        {
            _uncommittedEvents.Enqueue(@event);
        }
    }
}
