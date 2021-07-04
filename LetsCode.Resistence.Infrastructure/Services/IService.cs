using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LetsCode.Resistance.Domain;

namespace LetsCode.Resistance.Infrastructure.Services
{
    public interface IService<T> where T : class
    {
        Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<T> GetById(Guid id, CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
    }
}