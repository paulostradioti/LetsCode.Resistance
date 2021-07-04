using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LetsCode.Resistance.Infrastructure.Respositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));

        Task UpdateAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));

        Task DeleteAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));

        Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default(CancellationToken));

        Task SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<T> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull;

        Task<IEnumerable<T>> ListAsync(CancellationToken cancellationToken = default);
        IQueryable<T> AsQueryable(CancellationToken cancellationToken = default);
    }
}