using Core.Aggregates;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IAggregateRepository<T> where T : IAggregate
    {
        Task<T> Find(Guid id, CancellationToken cancellationToken);

        Task Add(T aggregate, CancellationToken cancellationToken);

        Task Update(T aggregate, CancellationToken cancellationToken);

        Task Delete(T aggregate, CancellationToken cancellationToken);
    }
}
