using System.Threading.Tasks;

namespace LetsCode.Resistance.Infrastructure.Services.Interfaces
{
    public interface IReportService
    {
        Task<object> TraitorsReport();
        Task<object> RebelsReport();
        Task<object> AverageResourceReport();
        Task<object> LossesReport();
    }
}