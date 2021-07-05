using LetsCode.Resistance.Domain;
using LetsCode.Resistance.Infrastructure.Repository.Base;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LetsCode.Resistance.Infrastructure.Repository.Interface
{
    public interface IPriceRepository : IRepository<Price>
    {
        Task<List<Price>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}