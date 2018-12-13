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
    public class WhenSortingHigh
    {
        private SortResponse _sortResponse;
        private Mock<IProductsRepository> _productsRepository;
        private List<Product> _model;

        [OneTimeSetUp]
        public async Task Setup()
        {
            _model = new List<Product>() {
                new Product { Price = 1 },
                new Product { Price = 4 },
                new Product { Price = 2 },
                new Product { Price = 3 }
            };

            _productsRepository = new Mock<IProductsRepository>();
            _productsRepository.Setup(r => r.GetProducts()).ReturnsAsync(_model);

            var handler = new GetSortRequestHandler(_productsRepository.Object, null);
            _sortResponse = await handler.Handle(new GetSortRequest { SortOption = "High" }, CancellationToken.None);
        }

        [Test]
        public void SortResponseShouldNotBeNull()
        {
            Assert.That(_sortResponse, Is.Not.Null);
            Assert.That(_sortResponse.Products.Count, Is.EqualTo(_model.Count));
        }

        [Test]
        public void SortShouldBeCorrect()
        {
            Assert.That(_sortResponse.Products[0].Price, Is.EqualTo(4));
            Assert.That(_sortResponse.Products[1].Price, Is.EqualTo(3));
            Assert.That(_sortResponse.Products[2].Price, Is.EqualTo(2));
            Assert.That(_sortResponse.Products[3].Price, Is.EqualTo(1));
        }

        [Test]
        public void ShouldCallApi()
        {
            _productsRepository.Verify(r => r.GetProducts(), Times.Once);
        }
    }
}