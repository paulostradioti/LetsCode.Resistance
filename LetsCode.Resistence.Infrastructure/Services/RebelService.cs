using AutoMapper;
using LetsCode.Resistance.Domain;
using LetsCode.Resistance.Infrastructure.Respositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LetsCode.Resistance.Infrastructure.Services.Base;

namespace LetsCode.Resistance.Infrastructure.Services
{
    public class RebelService : Service<Rebel>, IRebelService
    {
        private const int TreasonCount = 3;
        private readonly IRepository<Rebel> _repository;
        private readonly IRepository<Price> _priceRepository;
        private readonly IMapper _mapper;

        public RebelService(IRepository<Rebel> repository, IRepository<Price> priceRepository, IMapper mapper) : base(repository)
        {
            _repository = repository;
            _priceRepository = priceRepository;
            _mapper = mapper;
        }

        public override async Task<Rebel> CreateAsync(Rebel entity, CancellationToken cancellationToken = default)
        {
            var knownInventoryGroups = _priceRepository.AsQueryable().Select(x => x.ItemName).Distinct();
            entity.Inventory = entity.Inventory.Where(x => knownInventoryGroups.Contains(x.Name)).ToList();

            return await base.CreateAsync(entity, cancellationToken);
        }

        public override async Task<Rebel> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            return await _repository.AsQueryable().Include(x => x.Location)
                .Include(x => x.Inventory).FirstOrDefaultAsync(x => x.Id.Equals(id) && !x.IsTraitor, cancellationToken: cancellationToken);
        }

        public override async Task<IEnumerable<Rebel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _repository.AsQueryable().Include(x => x.Inventory).Include(x => x.Location)
                .Where(x => !x.IsTraitor).ToListAsync(cancellationToken);
        }

        public async Task<Rebel> UpdateRebelLocationAsync(Guid requestRebelId, Location location, CancellationToken cancellationToken = default)
        {
            var rebel = await _repository.AsQueryable().Include(x => x.Location)
                .Include(x => x.Inventory).FirstOrDefaultAsync(x => x.Id == requestRebelId && !x.IsTraitor, cancellationToken);

            if (rebel == null)
                return null;

            rebel.Location = location;
            await _repository.UpdateAsync(rebel, cancellationToken);
            return rebel;
        }

        public async Task<Rebel> PutAsync(Rebel request, CancellationToken cancellationToken = default)
        {
            var entity = await GetById(request.Id, cancellationToken);
            if (entity == null)
                return null;

            _mapper.Map(request, entity);

            await _repository.UpdateAsync(entity, cancellationToken);

            return entity;
        }

        public async Task<Rebel> ReportAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetByIdAsync(id, cancellationToken);
            if (entity == null)
                return null;

            entity.ReportCount++;
            entity.IsTraitor = entity.ReportCount >= TreasonCount;
            await _repository.UpdateAsync(entity, cancellationToken);

            return entity;
        }
    }
}