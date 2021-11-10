using Core.Projections;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.EF.Extensions
{
    internal static class DBSetEvenEntryExtensions
    {
        internal static async Task<T> AggregateStream<T>(
            this DbSet<EventEntry> set,
            Guid aggregateId,
            CancellationToken cancellationToken)
             where T : class, IProjection
        {
            var events = await set
                .Where(x => x.AggregateId == aggregateId)
                .OrderBy(x => x.Version)
                .ToListAsync(cancellationToken);

            var aggregate = (T)Activator.CreateInstance(typeof(T), true)!;

            foreach (var @event in events)
            {
                var eventData = @event.Deserialize();

                aggregate.When(eventData!);
            }

            return aggregate;
        }
    }
}
