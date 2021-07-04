using System.Threading.Tasks;

namespace LetsCode.Resistance.Infrastructure.Service.Interface
{
    public interface IReportService
    {
        Task<object> TraitorsReport();

        Task<object> RebelsReport();

        Task<object> AverageResourceReport();

        Task<object> LossesReport();
    }
}