using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IModelRepository<T> where T : class
    {
        Task<T> Find(Guid id, CancellationToken cancellationToken);

        IQueryable<T> Query();

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        Task SaveChanges(CancellationToken cancellationToken);
    }
}
