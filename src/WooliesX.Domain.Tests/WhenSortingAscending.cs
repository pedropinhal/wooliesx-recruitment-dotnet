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
    public class WhenSortingAscending
    {
        private SortResponse _sortResponse;
        private Mock<IProductsRepository> _productsRepository;
        private List<Product> _model;

        [OneTimeSetUp]
        public async Task Setup()
        {
            _model = new List<Product>() {
                new Product { Name = "Orange" },
                new Product { Name = "Apple" },
                new Product { Name = "Banana" },
                new Product { Name = "Zucchini" }
            };

            _productsRepository = new Mock<IProductsRepository>();
            _productsRepository.Setup(r => r.GetProducts()).ReturnsAsync(_model);

            var handler = new GetSortRequestHandler(_productsRepository.Object, null);
            _sortResponse = await handler.Handle(new GetSortRequest { SortOption = "Ascending" }, CancellationToken.None);
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
            Assert.That(_sortResponse.Products[0].Name, Is.EqualTo("Apple"));
            Assert.That(_sortResponse.Products[1].Name, Is.EqualTo("Banana"));
            Assert.That(_sortResponse.Products[2].Name, Is.EqualTo("Orange"));
            Assert.That(_sortResponse.Products[3].Name, Is.EqualTo("Zucchini"));
        }

        [Test]
        public void ShouldCallApi()
        {
            _productsRepository.Verify(r => r.GetProducts(), Times.Once);
        }
    }
}