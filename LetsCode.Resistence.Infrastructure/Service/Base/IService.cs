using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LetsCode.Resistance.Infrastructure.Service.Base
{
    public interface IService<T> where T : class
    {
        Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);

        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<T> GetById(Guid id, CancellationToken cancellationToken = default);

        Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}