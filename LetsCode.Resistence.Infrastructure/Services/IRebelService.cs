using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LetsCode.Resistance.Domain;
using LetsCode.Resistance.Infrastructure.RequestModels;

namespace LetsCode.Resistance.Infrastructure.Services
{
    public interface IRebelService : IService<Rebel>
    {

        Task<Rebel> UpdateRebelLocationAsync(Guid requestRebelId, Location entity, CancellationToken cancellationToken = default(CancellationToken));
        Task<Rebel> PutAsync(Rebel request, CancellationToken cancellationToken = default(CancellationToken));
    }
}
