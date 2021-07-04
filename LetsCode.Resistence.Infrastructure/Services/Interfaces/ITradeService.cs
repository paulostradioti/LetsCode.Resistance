using LetsCode.Resistance.Infrastructure.RequestModels;
using System.Threading.Tasks;

namespace LetsCode.Resistance.Infrastructure.Services.Interfaces
{
    public interface ITradeService
    {
        Task Trade(TradeRequestModel request);
    }
}