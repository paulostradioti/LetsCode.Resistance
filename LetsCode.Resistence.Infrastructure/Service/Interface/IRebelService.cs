using LetsCode.Resistance.Domain;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LetsCode.Resistance.Infrastructure.Service.Interface
{
    public interface IRebelService
    {
        Task<Rebel> CreateAsync(Rebel entity, CancellationToken cancellationToken = default);
        Task<Rebel> GetById(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Rebel>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Rebel> UpdateRebelLocationAsync(Guid requestRebelId, Location entity, CancellationToken cancellationToken = default);
        Task<Rebel> PutAsync(Rebel request, CancellationToken cancellationToken = default);
        Task<Rebel> ReportAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}