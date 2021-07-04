﻿using LetsCode.Resistance.Domain;
using LetsCode.Resistance.Infrastructure.Respositories;
using LetsCode.Resistance.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace LetsCode.Resistance.Infrastructure.Services
{
    public class ReportService : IReportService
    {
        private readonly IRepository<Rebel> _rebelRepository;
        private readonly IRepository<Price> _priceRepository;

        public ReportService(IRepository<Rebel> rebelRepository, IRepository<Price> priceRepository)
        {
            _rebelRepository = rebelRepository;
            _priceRepository = priceRepository;
        }

        public async Task<object> TraitorsReport()
        {
            var totalCount = await _rebelRepository.AsQueryable().CountAsync();
            var traitorCount = await _rebelRepository.AsQueryable().CountAsync(x => x.IsTraitor);
            var percentage = totalCount > 0 ? traitorCount / totalCount : 0;
            return new
            {
                totalCount,
                traitorCount,
                percentage = percentage.ToString("P", new CultureInfo("en-US")),
                message = $"There {(traitorCount is 0 or > 1 ? "are" : "is")} {(traitorCount is 0 ? "no" : traitorCount)} {(traitorCount is 0 or > 1 ? "Traitors" : "Traitor")} out of {totalCount} total records"
            };
        }

        public async Task<object> RebelsReport()
        {
            var totalCount = await _rebelRepository.AsQueryable().CountAsync();
            var rebelsCount = await _rebelRepository.AsQueryable().CountAsync(x => !x.IsTraitor);
            var percentage = totalCount > 0 ? rebelsCount / totalCount : 0;
            return new
            {
                totalCount,
                rebelsCount,
                percentage = percentage.ToString("P", new CultureInfo("en-US")),
                message = $"There {(rebelsCount is 0 or > 1 ? "are" : "is")} {(rebelsCount is 0 ? "no" : rebelsCount)} {(rebelsCount is 0 or > 1 ? "Rebels" : "Rebel")} out of {totalCount} total records"
            };
        }

        public async Task<object> AverageResourceReport()
        {
            var rebelsCount = await _rebelRepository.AsQueryable().CountAsync(x => !x.IsTraitor);

            var rebelsInventory = _rebelRepository.AsQueryable().Include(x => x.Inventory)
                .Where(x => !x.IsTraitor)
                .SelectMany(x => x.Inventory)
                .GroupBy(g => g.Name)
                .Select(g => new { Name = g.Key, Quantity = (double)g.Sum(x => x.Quantity)/rebelsCount }).ToList();

            return rebelsInventory;
        }

        public async Task<object> LossesReport()
        {
            var prices = _priceRepository.AsQueryable().Distinct().ToList();

            var rebelsInventory = _rebelRepository.AsQueryable().Include(x => x.Inventory)
                .Where(x => x.IsTraitor)
                .SelectMany(x => x.Inventory)
                .GroupBy(g => g.Name)
                .Select(g => new {Name = g.Key, Quantity = g.Sum(x => x.Quantity)}).ToList();

            var lossesWithPrice = from item in rebelsInventory
                join price in prices on item.Name equals price.ItemName
                select new { ItemName = item.Name, LossInQuantity = item.Quantity, PartialLossInPoints = price.PriceInPoints * item.Quantity };

            var partialLosses = lossesWithPrice.ToList();
            var totalLoss = new {PartialLosses = partialLosses, TotalLossInPoints = partialLosses.Sum(x => x.PartialLossInPoints) };

            return await Task.FromResult(totalLoss);
        }
    }
}