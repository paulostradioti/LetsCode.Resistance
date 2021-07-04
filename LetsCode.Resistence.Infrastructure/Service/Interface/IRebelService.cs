using System;
using System.Threading;
using System.Threading.Tasks;
using LetsCode.Resistance.Domain;
using LetsCode.Resistance.Infrastructure.Service.Base;

namespace LetsCode.Resistance.Infrastructure.Service.Interface
{
    public interface IRebelService : IService<Rebel>
    {
        Task<Rebel> UpdateRebelLocationAsync(Guid requestRebelId, Location entity, CancellationToken cancellationToken = default);

        Task<Rebel> PutAsync(Rebel request, CancellationToken cancellationToken = default);

        Task<Rebel> ReportAsync(Guid id, CancellationToken cancellationToken = default);
    }
}