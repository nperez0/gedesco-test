using Core.Aggregates;
using Core.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class IAggregateRepositoryExtensions
    {
        public static async Task<T> Get<T>(this IAggregateRepository<T> repository, Guid id, CancellationToken cancellationToken = default) where T : IAggregate
        {
            var entity = await repository.Find(id, cancellationToken);

            return entity ?? throw new InvalidOperationException($"Failed to locate {typeof(T).Name} with id {id}.");
        }

        public static async Task<Unit> GetAndUpdate<T>(this IAggregateRepository<T> repository, Guid id, Action<T> action, CancellationToken cancellationToken = default) where T : IAggregate
        {
            var entity = await repository.Get<T>(id, cancellationToken);

            action(entity);

            await repository.Update(entity, cancellationToken);

            return Unit.Value;
        }
    }
}
