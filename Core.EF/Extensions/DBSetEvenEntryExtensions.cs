using Core.Aggregates;
using Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
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
             where T : class, IAggregate
        {
            var events = await set
                .Where(x => x.AggregateId == aggregateId)
                .OrderBy(x => x.Version)
                .ToListAsync(cancellationToken);

            var aggregate = Construct<T>(
                aggregateId
                    .GetType()
                    .AsArray(), 
                aggregateId
                    .AsArray()
                    .Cast<object>()
                    .ToArray());

            foreach (var @event in events)
            {
                var eventData = @event.Deserialize();

                aggregate.When(eventData!);
            }

            return aggregate;
        }

        private static T Construct<T>(Type[] paramTypes, object[] paramValues)
        {
            Type t = typeof(T);

            ConstructorInfo ci = t.GetConstructor(
                BindingFlags.Instance | BindingFlags.NonPublic,
                null, paramTypes, null);

            return (T)ci.Invoke(paramValues);
        }

    }
}
