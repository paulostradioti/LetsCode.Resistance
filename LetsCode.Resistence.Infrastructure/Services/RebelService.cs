using LetsCode.Resistance.Domain;
using LetsCode.Resistance.Infrastructure.Respositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LetsCode.Resistance.Infrastructure.RequestModels;

namespace LetsCode.Resistance.Infrastructure.Services
{
    public class RebelService : Service<Rebel>, IRebelService
    {
        private readonly IRepository<Rebel> _repository;
        private readonly IMapper _mapper;

        public RebelService(IRepository<Rebel> repository, IMapper mapper) : base(repository)
        {
            _repository = repository;
            _mapper = mapper;
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

        public async Task<Rebel> PutAsync(Rebel request, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entity = await GetById(request.Id, cancellationToken);
            if (entity == null)
                return null;

            _mapper.Map(request, entity);
            await _repository.UpdateAsync(entity, cancellationToken);
            
            return entity;
        }
    }
}