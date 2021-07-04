using System;
using System.Collections.Generic;
using LetsCode.Resistance.Infrastructure.Respositories;
using System.Threading;
using System.Threading.Tasks;
using LetsCode.Resistance.Domain;
using Microsoft.EntityFrameworkCore;

namespace LetsCode.Resistance.Infrastructure.Services
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly IRepository<T> _repository;

        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _repository.AddAsync(entity, cancellationToken);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _repository.ListAsync(cancellationToken);
        }

        public virtual async Task<T> GetById(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _repository.GetByIdAsync(id, cancellationToken);
        }

        public virtual async Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entity = await _repository.GetByIdAsync(id, cancellationToken);

            if (entity == null)
                return false;

            await _repository.DeleteAsync(entity, cancellationToken);
            return true;
        }
    }
}