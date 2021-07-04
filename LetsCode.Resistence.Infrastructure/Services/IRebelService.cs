using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LetsCode.Resistance.Domain;

namespace LetsCode.Resistance.Infrastructure.Services
{
    public interface IRebelService : IService<Rebel>
    {

        Task<Rebel> UpdateRebelLocationAsync(Guid requestRebelId, Location entity, CancellationToken cancellationToken = default(CancellationToken));
    }
}
