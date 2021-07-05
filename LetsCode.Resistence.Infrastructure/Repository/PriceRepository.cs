using LetsCode.Resistance.Domain;
using LetsCode.Resistance.Infrastructure.Repository.Base;
using LetsCode.Resistance.Infrastructure.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LetsCode.Resistance.Infrastructure.Repository
{
    public class PriceRepository : Repository<Price>, IPriceRepository
    {
        private readonly AppDbContext _dbContext;

        public PriceRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Price>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Prices.ToListAsync(cancellationToken);
        }
    }
}