using LetsCode.Resistance.Domain;
using LetsCode.Resistance.Infrastructure.Repository.Base;
using LetsCode.Resistance.Infrastructure.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace LetsCode.Resistance.Infrastructure.Repository
{
    public class RebelRepository : Repository<Rebel>, IRebelRepository
    {
        private readonly AppDbContext _dbContext;

        public RebelRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<Rebel> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Rebels.Include(x => x.Inventory).Include(x => x.Location)
                .FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
        }
    }
}