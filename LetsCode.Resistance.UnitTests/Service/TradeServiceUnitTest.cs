using AutoFixture;
using FluentAssertions;
using LetsCode.Resistance.Domain;
using LetsCode.Resistance.Infrastructure.Repository.Interface;
using LetsCode.Resistance.Infrastructure.RequestModel;
using LetsCode.Resistance.Infrastructure.Service;
using LetsCode.Resistance.Infrastructure.Service.Interface;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LetsCode.Resistance.UnitTests.Service
{
    public class TradeServiceUnitTest
    {
        private readonly Fixture _fixture = new();
        private ITradeService _sut;

        [Test]
        public async Task BuyerNotFound()
        {
            var trade = _fixture.Build<TradeRequestModel>().Without(x => x.Buyer).Create();
            _sut = CreateService();
            await _sut.Invoking(x => x.Trade(trade)).Should().ThrowAsync<TradeException>().WithMessage("Buyer Not Found");
        }

        [Test]
        public async Task SellerNotFound()
        {
            var buyer = _fixture.Build<Rebel>().With(x => x.IsTraitor, false).Create();
            var buyerModel = _fixture.Build<TradePartRequestModel>().With(x => x.RebelId, buyer.Id).Create();

            var trade = _fixture.Build<TradeRequestModel>()
                .With(x => x.Buyer, buyerModel)
                .Without(x => x.Seller)
                .Create();

            _sut = CreateService(buyer);
            await _sut.Invoking(x => x.Trade(trade)).Should().ThrowAsync<TradeException>().WithMessage("Seller Not Found");
        }

        [TestCase("Buyer", true)]
        [TestCase("Seller", false)]
        public async Task TraitorsCantTrade(string partyType, bool isBuyer)
        {
            var buyer = _fixture.Build<Rebel>().With(x => x.IsTraitor, isBuyer).Create();
            var seller = _fixture.Build<Rebel>().With(x => x.IsTraitor, !isBuyer).Create();

            var buyerModel = _fixture.Build<TradePartRequestModel>().With(x => x.RebelId, buyer.Id).Create();
            var sellerModel = _fixture.Build<TradePartRequestModel>().With(x => x.RebelId, seller.Id).Create();

            var trade = _fixture.Build<TradeRequestModel>()
                .With(x => x.Buyer, buyerModel)
                .With(x => x.Seller, sellerModel)
                .Create();

            _sut = CreateService(buyer, seller);
            await _sut.Invoking(x => x.Trade(trade)).Should().ThrowAsync<TradeException>().WithMessage($"{partyType} is a Traitor and Can't Trade");
        }

        [Test]
        public async Task PriceMismatch()
        {
            var buyer = BuildBuyer();
            var seller = BuildSeller();

            var buyerModel = ToModel(buyer);
            var sellerModel = ToModel(seller);

            var trade = _fixture.Build<TradeRequestModel>()
                .With(x => x.Buyer, buyerModel)
                .With(x => x.Seller, sellerModel)
                .Create();

            _sut = CreateService(buyer, seller);
            await _sut.Invoking(x => x.Trade(trade)).Should().ThrowAsync<TradeException>().WithMessage("Both parties should trade the same amount of points.");

            Rebel BuildBuyer()
            {
                var rebelId = Guid.NewGuid();
                var inventory = new List<InventoryItem>
                {
                    new() { Name = "Água", Quantity = 1, RebelId = rebelId, Id = Guid.NewGuid() }
                };

                return _fixture.Build<Rebel>().With(x => x.Inventory, inventory).With(x => x.Id, rebelId)
                    .With(x => x.IsTraitor, false).Create();
            }

            Rebel BuildSeller()
            {
                var rebelId = Guid.NewGuid();
                var inventory = new List<InventoryItem>
                {
                    new() { Name = "Comida", Quantity = 1, RebelId = rebelId, Id = Guid.NewGuid() }
                };

                return _fixture.Build<Rebel>().With(x => x.Inventory, inventory).With(x => x.Id, rebelId)
                    .With(x => x.IsTraitor, false).Create();
            }
        }

        [Test]
        public async Task TradeSuccess()
        {
            var buyer = BuildBuyer();
            var seller = BuildSeller();

            var buyerModel = ToModel(buyer);
            var sellerModel = ToModel(seller);

            var trade = _fixture.Build<TradeRequestModel>()
                .With(x => x.Buyer, buyerModel)
                .With(x => x.Seller, sellerModel)
                .Create();

            _sut = CreateService(buyer, seller);
            await _sut.Invoking(x => x.Trade(trade)).Should().NotThrowAsync();

            Rebel BuildBuyer()
            {
                var rebelId = Guid.NewGuid();
                var inventory = new List<InventoryItem>
                {
                    new() { Name = "Água", Quantity = 1, RebelId = rebelId, Id = Guid.NewGuid() }
                };

                return _fixture.Build<Rebel>().With(x => x.Inventory, inventory).With(x => x.Id, rebelId)
                    .With(x => x.IsTraitor, false).Create();
            }

            Rebel BuildSeller()
            {
                var rebelId = Guid.NewGuid();
                var inventory = new List<InventoryItem>
                {
                    new() { Name = "Comida", Quantity = 2, RebelId = rebelId, Id = Guid.NewGuid() }
                };

                return _fixture.Build<Rebel>().With(x => x.Inventory, inventory).With(x => x.Id, rebelId)
                    .With(x => x.IsTraitor, false).Create();
            }
        }

        [TestCase("Buyer", "Água", 0, 2)]
        [TestCase("Seller", "Comida", 1, 0)]
        public async Task InsufficientInventory(string partyType, string inventoryItem, int buyerInventoryCount, int sellerInventoryCount)
        
        {
            var buyer = BuildBuyer(buyerInventoryCount);
            var seller = BuildSeller(sellerInventoryCount);

            var buyerModel = BuildBuyerModel();
            var sellerModel = BuildSellerModel();

            var trade = _fixture.Build<TradeRequestModel>()
                .With(x => x.Buyer, buyerModel)
                .With(x => x.Seller, sellerModel)
                .Create();

            _sut = CreateService(buyer, seller);
            await _sut.Invoking(x => x.Trade(trade)).Should().ThrowAsync<TradeException>().WithMessage($"{partyType} doesn't have enough {inventoryItem} in Inventory");

            Rebel BuildBuyer(int inventoryCount)
            {
                var rebelId = Guid.NewGuid();
                var inventory = new List<InventoryItem>
                {
                    new() { Name = "Água", Quantity = inventoryCount, RebelId = rebelId, Id = Guid.NewGuid() }
                };

                return _fixture.Build<Rebel>().With(x => x.Inventory, inventory).With(x => x.Id, rebelId)
                    .With(x => x.IsTraitor, false).Create();
            }

            Rebel BuildSeller(int inventoryCount)
            {
                var rebelId = Guid.NewGuid();
                var inventory = new List<InventoryItem>
                {
                    new() { Name = "Comida", Quantity = inventoryCount, RebelId = rebelId, Id = Guid.NewGuid() }
                };

                return _fixture.Build<Rebel>().With(x => x.Inventory, inventory).With(x => x.Id, rebelId)
                    .With(x => x.IsTraitor, false).Create();
            }

            TradePartRequestModel BuildBuyerModel()
            {
                return new()
                {
                    RebelId = buyer.Id,
                    TradingItems = new List<InventoryItemModel> { new InventoryItemModel { Name = "Água", Quantity = 1 } }
                };
            }

            TradePartRequestModel BuildSellerModel()
            {
                return new()
                {
                    RebelId = seller.Id,
                    TradingItems = new List<InventoryItemModel> { new InventoryItemModel { Name = "Comida", Quantity = 2 } }
                };
            }
        }

        private static TradePartRequestModel ToModel(Rebel buyer)
        {
            return new TradePartRequestModel
            {
                RebelId = buyer.Id,
                TradingItems = buyer.Inventory.Select(x => new InventoryItemModel { Quantity = x.Quantity, Name = x.Name })
            };
        }

        private static ITradeService CreateService()
        {
            var rebelRepository = new Mock<IRebelRepository>();
            var priceRepository = new Mock<IPriceRepository>();

            rebelRepository.Setup(x => x.GetByIdAsync<Guid?>(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync((Rebel)null);

            return new TradeService(rebelRepository.Object, priceRepository.Object);
        }

        private static ITradeService CreateService(Rebel rebel)
        {
            var rebelRepository = new Mock<IRebelRepository>();
            var priceRepository = new Mock<IPriceRepository>();

            rebelRepository.Setup(x => x.GetByIdAsync<Guid?>(rebel.Id, It.IsAny<CancellationToken>())).ReturnsAsync(rebel);

            return new TradeService(rebelRepository.Object, priceRepository.Object);
        }

        private static ITradeService CreateService(Rebel buyer, Rebel seller)
        {
            var rebelRepository = new Mock<IRebelRepository>();
            var priceRepository = new Mock<IPriceRepository>();

            rebelRepository.Setup(x => x.GetByIdAsync<Guid?>(buyer.Id, It.IsAny<CancellationToken>())).ReturnsAsync(buyer);
            rebelRepository.Setup(x => x.GetByIdAsync<Guid?>(seller.Id, It.IsAny<CancellationToken>())).ReturnsAsync(seller);

            priceRepository.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Price>(Prices()));

            return new TradeService(rebelRepository.Object, priceRepository.Object);
        }

        private static IEnumerable<Price> Prices()
        {
            return new[]
            {
                new Price
                {
                    Id = Guid.NewGuid(),
                    ItemName = "Arma",
                    PriceInPoints = 4
                },
                new Price
                {
                    Id = Guid.NewGuid(),
                    ItemName = "Munição",
                    PriceInPoints = 3
                },
                new Price
                {
                    Id = Guid.NewGuid(),
                    ItemName = "Água",
                    PriceInPoints = 2
                },
                new Price
                {
                    Id = Guid.NewGuid(),
                    ItemName = "Comida",
                    PriceInPoints = 1
                },
            };
        }
    }
}