using LetsCode.Resistance.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LetsCode.Resistance.Infrastructure.Services
{
    public interface IRebelService : IService<Rebel>
    {
        Task<Rebel> UpdateRebelLocationAsync(Guid requestRebelId, Location entity, CancellationToken cancellationToken = default);

        Task<Rebel> PutAsync(Rebel request, CancellationToken cancellationToken = default);

        Task<Rebel> ReportAsync(Guid id, CancellationToken cancellationToken = default);
    }
}