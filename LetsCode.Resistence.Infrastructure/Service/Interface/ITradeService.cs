using System.Threading.Tasks;
using LetsCode.Resistance.Infrastructure.RequestModel;

namespace LetsCode.Resistance.Infrastructure.Service.Interface
{
    public interface ITradeService
    {
        Task Trade(TradeRequestModel request);
    }
}