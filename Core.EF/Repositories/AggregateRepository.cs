using Core.Aggregates;
using Core.EF.Extensions;
using Core.Events;
using Core.Ids;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.EF.Repositories
{
    public class AggregateRepository<T> : IAggregateRepository<T>
        where T : class, IAggregate
    {
        private readonly DbContext _context;
        private readonly IEventBus _eventBus;
        private readonly IIdGenerator _idGenerator;

        public AggregateRepository(DbContext context, IEventBus eventBus, IIdGenerator idGenerator)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _idGenerator = idGenerator ?? throw new ArgumentNullException(nameof(idGenerator));
        }

        public Task<T> Find(Guid id, CancellationToken cancellationToken)
            => _context.Set<EventEntry>().AggregateStream<T>(id, cancellationToken);

        public Task Add(T aggregate, CancellationToken cancellationToken)
            => Store(aggregate, cancellationToken);

        public Task Update(T aggregate, CancellationToken cancellationToken)
            => Store(aggregate, cancellationToken);

        public Task Delete(T aggregate, CancellationToken cancellationToken)
            => Store(aggregate, cancellationToken);

        private async Task Store(T aggregate, CancellationToken cancellationToken)
        {
            var events = aggregate.DequeueUncommittedEvents();

            var eventsToStore = events
                .Select(x => x.ToEventEntry(
                    _idGenerator.New(), 
                    aggregate.Id, 
                    aggregate.Version))
                .ToArray();

            await _context.Set<EventEntry>().AddRangeAsync(eventsToStore, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            await _eventBus.Publish(events);
        }
    }
}
