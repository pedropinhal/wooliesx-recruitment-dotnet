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
        private ShopperHistory _model;

        [OneTimeSetUp]
        public async Task Setup()
        {
            _model = new ShopperHistory {
                CustomerId = 123,
                Products = 
                new List<Product>() {
                new Product { Name = "Item1" },
                new Product { Name = "Item2" }
            }};

            _shopperHistoryRepository = new Mock<IShopperHistoryRepository>();
            _shopperHistoryRepository.Setup(r => r.GetCustomerHistory()).ReturnsAsync(_model);

            var handler = new GetSortRequestHandler(null, _shopperHistoryRepository.Object);
            _sortResponse = await handler.Handle(new GetSortRequest { SortOption = "Recommended" }, CancellationToken.None);
        }

        [Test]
        public void SortResponseShouldNotBeNull()
        {
            Assert.That(_sortResponse, Is.Not.Null);
            Assert.That(_sortResponse.Products.Count, Is.EqualTo(_model.Products.Count));
        }

        [Test]
        public void SortShouldBeCorrect()
        {
            Assert.That(_sortResponse.Products[0].Name, Is.EqualTo("Item1"));
            Assert.That(_sortResponse.Products[1].Name, Is.EqualTo("Item2"));
        }

        [Test]
        public void ShouldCallApi()
        {
            _shopperHistoryRepository.Verify(r => r.GetCustomerHistory(), Times.Once);
        }
    }
}