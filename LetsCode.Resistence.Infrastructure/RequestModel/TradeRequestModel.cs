using System;
using System.Collections.Generic;

namespace LetsCode.Resistance.Infrastructure.RequestModel
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