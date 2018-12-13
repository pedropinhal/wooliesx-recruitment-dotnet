using System;
using System.Threading;
using System.Threading.Tasks;
using WooliesX.Domain.Requests;
using WooliesX.Domain.Responses;
using NUnit.Framework;
using WooliesX.Domain.Ports;
using Moq;
using WooliesX.Domain.Models;
using System.Collections.Generic;

namespace WooliesX.Domain.Tests
{
    [TestFixture]
    public class WhenSortingRecommended
    {
        private SortResponse _sortResponse;
        private Mock<IShopperHistoryRepository> _shopperHistoryRepository;
        private List<ShopperHistory> _model;

        [OneTimeSetUp]
        public async Task Setup()
        {
            _model = new List<ShopperHistory> {
                new ShopperHistory {
                    CustomerId = 1,
                    Products =
                    new List<Product>() {
                        new Product { Name = "Item1", Quantity = 1 },
                        new Product { Name = "Item2", Quantity = 1 }
                    }
                },
                new ShopperHistory {
                    CustomerId = 2,
                    Products =
                    new List<Product>() {
                        new Product { Name = "Item3", Quantity = 1 },
                        new Product { Name = "Item4", Quantity = 1 }
                    }
                },
                new ShopperHistory {
                    CustomerId = 3,
                    Products =
                    new List<Product>() {
                        new Product { Name = "Item3", Quantity = 2 }
                    }
                },
                new ShopperHistory {
                    CustomerId = 1,
                    Products =
                    new List<Product>() {
                        new Product { Name = "Item2", Quantity = 1 }
                    }
                }
            };

            _shopperHistoryRepository = new Mock<IShopperHistoryRepository>();
            _shopperHistoryRepository.Setup(r => r.GetCustomerHistory()).ReturnsAsync(_model);

            var handler = new GetSortRequestHandler(null, _shopperHistoryRepository.Object);
            _sortResponse = await handler.Handle(new GetSortRequest { SortOption = "Recommended" }, CancellationToken.None);
        }

        [Test]
        public void SortResponseShouldNotBeNull()
        {
            Assert.That(_sortResponse, Is.Not.Null);
            Assert.That(_sortResponse.Products.Count, Is.EqualTo(4));
        }

        [Test]
        public void SortShouldBeCorrect()
        {
            Assert.That(_sortResponse.Products[0].Name, Is.EqualTo("Item3"));
            Assert.That(_sortResponse.Products[0].Quantity, Is.EqualTo(3));

            Assert.That(_sortResponse.Products[1].Name, Is.EqualTo("Item2"));
            Assert.That(_sortResponse.Products[1].Quantity, Is.EqualTo(2));

            Assert.That(_sortResponse.Products[2].Name, Is.EqualTo("Item1"));
            Assert.That(_sortResponse.Products[2].Quantity, Is.EqualTo(1));

            Assert.That(_sortResponse.Products[3].Name, Is.EqualTo("Item4"));
            Assert.That(_sortResponse.Products[3].Quantity, Is.EqualTo(1));
        }

        [Test]
        public void ShouldCallApi()
        {
            _shopperHistoryRepository.Verify(r => r.GetCustomerHistory(), Times.Once);
        }
    }
}