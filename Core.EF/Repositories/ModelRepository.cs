using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.EF.Repositories
{
    public class ModelRepository<T> : IModelRepository<T>
        where T : class
    {
        private readonly DbContext _context;

        public ModelRepository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<T> Find(Guid id, CancellationToken cancellationToken)
            => _context.Set<T>().FindAsync(id).AsTask();

        public IQueryable<T> Query()
            => _context.Set<T>();

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public Task SaveChanges(CancellationToken cancellationToken)
            => _context.SaveChangesAsync(cancellationToken);
    }
}
