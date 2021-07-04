using LetsCode.Resistance.Domain;
using LetsCode.Resistance.Infrastructure.Respositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LetsCode.Resistance.Infrastructure.Services
{
    public class RebelService : Service<Rebel>, IRebelService
    {
        private readonly IRepository<Rebel> _repository;

        public RebelService(IRepository<Rebel> repository) : base(repository)
        {
            _repository = repository;
        }

        public override async Task<Rebel> GetById(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _repository.AsQueryable().Include(x => x.Location).FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken: cancellationToken);
        }

        public override async Task<IEnumerable<Rebel>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _repository.AsQueryable().Include(x => x.Location).ToListAsync(cancellationToken);
        }

        public async Task<Rebel> UpdateRebelLocationAsync(Guid requestRebelId, Location location, CancellationToken cancellationToken = default(CancellationToken))
        {
            var rebel = await _repository.GetByIdAsync(requestRebelId, cancellationToken);

            if (rebel == null)
                return null;

            rebel.Location = location;
            await _repository.UpdateAsync(rebel, cancellationToken);
            return rebel;
        }
    }
}