﻿using LetsCode.Resistance.Domain;
using LetsCode.Resistance.Infrastructure.Repository.Base;
using LetsCode.Resistance.Infrastructure.Repository.Interface;
using LetsCode.Resistance.Infrastructure.RequestModel;
using LetsCode.Resistance.Infrastructure.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsCode.Resistance.Infrastructure.Service
{
    public class TradeService : ITradeService
    {
        private readonly IRebelRepository _rebelRepository;
        private readonly IPriceRepository _priceRepository;

        public TradeService(IRebelRepository rebelRepository, IPriceRepository priceRepository)
        {
            _rebelRepository = rebelRepository;
            _priceRepository = priceRepository;
        }

        public async Task Trade(TradeRequestModel request)
        {
            var buyerId = request?.Buyer?.RebelId;
            var sellerId = request?.Seller?.RebelId;

            var buyer = await _rebelRepository.GetByIdAsync(buyerId);
            if (buyer == null)
                throw new TradeException("Buyer Not Found");
            if (buyer.IsTraitor)
                throw new TradeException("Buyer is a Traitor and Can't Trade");

            var seller = await _rebelRepository.GetByIdAsync(sellerId);
            if (seller == null)
                throw new TradeException("Seller Not Found");
            if (seller.IsTraitor)
                throw new TradeException("Seller is a Traitor and Can't Trade");

            var prices = await _priceRepository.GetAllAsync();
            var sellerItems = request?.Seller?.TradingItems?.ToList() ?? new List<InventoryItemModel>();
            var buyerItems = request?.Buyer?.TradingItems?.ToList() ?? new List<InventoryItemModel>();

            var sellingItemsWithPrice = from item in sellerItems
                                        join price in prices on item.Name equals price.ItemName
                                        select new { ItemName = item.Name, item.Quantity, Price = price.PriceInPoints };

            var buyingItemsWithPrice = from item in buyerItems
                                       join price in prices on item.Name equals price.ItemName
                                       select new { ItemName = item.Name, item.Quantity, Price = price.PriceInPoints };

            var sellingPoints = sellingItemsWithPrice.Sum(x => x.Price * x.Quantity);
            var buyingPoints = buyingItemsWithPrice.Sum(x => x.Price * x.Quantity);

            if (sellingPoints != buyingPoints)
                throw new TradeException("Both parties should trade the same amount of points.");

            sellerItems.ForEach(sellingItem =>
            {
                var available = seller.Inventory
                    .Where(x => x.Name.Equals(sellingItem.Name, StringComparison.InvariantCultureIgnoreCase))
                    .Sum(x => x.Quantity);
                if (available < sellingItem.Quantity)
                    throw new TradeException($"Seller doesn't have enough {sellingItem.Name} in Inventory");
            });

            buyerItems.ForEach(buyingItems =>
            {
                var available = buyer.Inventory
                    .Where(x => x.Name.Equals(buyingItems.Name, StringComparison.InvariantCultureIgnoreCase))
                    .Sum(x => x.Quantity);
                if (available < buyingItems.Quantity)
                    throw new TradeException($"Buyer doesn't have enough {buyingItems.Name} in Inventory");
            });

            sellerItems.ForEach(soldItem =>
            {
                var sellerInventoryItem = seller.Inventory.FirstOrDefault(x =>
                    x.Name.Equals(soldItem.Name, StringComparison.InvariantCultureIgnoreCase)) ?? new InventoryItem();
                sellerInventoryItem.Quantity -= soldItem.Quantity;

                if (sellerInventoryItem.Quantity == 0 && seller.Inventory.Contains(sellerInventoryItem))
                    seller.Inventory.Remove(sellerInventoryItem);

                var buyerInventoryItem = buyer.Inventory.FirstOrDefault(x =>
                    x.Name.Equals(soldItem.Name, StringComparison.InvariantCultureIgnoreCase)) ?? new InventoryItem
                    {
                        Name = soldItem.Name,
                        Quantity = 0,
                        RebelId = buyer.Id
                    };
                buyerInventoryItem.Quantity += soldItem.Quantity;
                if (!buyer.Inventory.Contains(buyerInventoryItem))
                    buyer.Inventory.Add(buyerInventoryItem);
            });

            buyerItems.ForEach(boughtItem =>
            {
                var buyerInventoryItem = buyer.Inventory.FirstOrDefault(x => x.Name.Equals(boughtItem.Name, StringComparison.InvariantCultureIgnoreCase)) ?? new InventoryItem();
                buyerInventoryItem.Quantity -= boughtItem.Quantity;

                if (buyerInventoryItem.Quantity == 0 && buyer.Inventory.Contains(buyerInventoryItem))
                    buyer.Inventory.Remove(buyerInventoryItem);

                var sellerInventoryItem = seller.Inventory.FirstOrDefault(x =>
                    x.Name.Equals(boughtItem.Name, StringComparison.InvariantCultureIgnoreCase)) ?? new InventoryItem
                    {
                        Name = boughtItem.Name,
                        Quantity = 0,
                        RebelId = buyer.Id
                    };

                sellerInventoryItem.Quantity += boughtItem.Quantity;
                if (!seller.Inventory.Contains(sellerInventoryItem))
                    seller.Inventory.Add(sellerInventoryItem);
            });

            await _rebelRepository.UpdateAsync(seller);
            await _rebelRepository.UpdateAsync(buyer);
        }
    }
}