using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LetsCode.Resistance.Domain;
using LetsCode.Resistance.Infrastructure.Repository;
using LetsCode.Resistance.Infrastructure.Repository.Base;
using LetsCode.Resistance.Infrastructure.Repository.Interface;
using LetsCode.Resistance.Infrastructure.Service.Base;
using LetsCode.Resistance.Infrastructure.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace LetsCode.Resistance.Infrastructure.Service
{
    public class RebelService : IRebelService
    {
        private readonly IRepository<Rebel> _repository;
        private readonly IPriceRepository _priceRepository;
        private readonly IMapper _mapper;

        public RebelService(IRebelRepository repository, IPriceRepository priceRepository, IMapper mapper)
        {
            _repository = repository;
            _priceRepository = priceRepository;
            _mapper = mapper;
        }

        public async Task<Rebel> CreateAsync(Rebel entity, CancellationToken cancellationToken = default)
        {
            var prices = await _priceRepository.GetAllAsync(cancellationToken);
            var inventoryItemNames = prices.Select(x => x.ItemName);
            entity.Inventory = entity.Inventory.Where(x => inventoryItemNames.Contains(x.Name)).ToList();

            return await _repository.AddAsync(entity, cancellationToken);
        }

        public async Task<Rebel> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            return await _repository.AsQueryable().Include(x => x.Location)
                .Include(x => x.Inventory).FirstOrDefaultAsync(x => x.Id.Equals(id) && !x.IsTraitor, cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<Rebel>> GetAllAsync(CancellationToken cancellationToken = default)
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
            var rebel = await _repository.GetByIdAsync(id, cancellationToken);
            if (rebel == null) return null;

            rebel.Report();
            await _repository.UpdateAsync(rebel, cancellationToken);

            return rebel;
        }

        public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetByIdAsync(id, cancellationToken);

            if (entity == null)
                return false;

            await _repository.DeleteAsync(entity, cancellationToken);
            return true;
        }
    }
}