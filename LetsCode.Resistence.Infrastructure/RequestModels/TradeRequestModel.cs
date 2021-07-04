using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace LetsCode.Resistance.Infrastructure.RequestModels
{
    public class TradeRequestModel
    {
        public TradePartRequestModel Buyer { get; set; }
        public TradePartRequestModel Seller { get; set; }
    }

    public class TradePartRequestModel
    {
        public Guid RebelId { get; set; }
        public IEnumerable<InventoryItemModel> TradingItems { get; set; }
    }
}
