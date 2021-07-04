using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LetsCode.Resistance.Infrastructure.Repository;

namespace LetsCode.Resistance.Infrastructure.Service.Base
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly IRepository<T> _repository;

        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
        {
            return await _repository.AddAsync(entity, cancellationToken);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _repository.ListAsync(cancellationToken);
        }

        public virtual async Task<T> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            return await _repository.GetByIdAsync(id, cancellationToken);
        }

        public virtual async Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetByIdAsync(id, cancellationToken);

            if (entity == null)
                return false;

            await _repository.DeleteAsync(entity, cancellationToken);
            return true;
        }
    }
}