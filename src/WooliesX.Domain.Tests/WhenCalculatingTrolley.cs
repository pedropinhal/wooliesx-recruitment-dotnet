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
    public class WhenCalculatingTrolley
    {
        private decimal calculateResponse;
        private CalculateTrolleyRequest _request;

        [OneTimeSetUp]
        public async Task Setup()
        {
            _request = new CalculateTrolleyRequest
            {
                Products = new List<Product>
                {
                    new Product { Name = "Item1", Price = 4 },
                    new Product { Name = "Item2", Price = 2 }
                },
                Quantities = new List<Product>
                {
                    new Product { Name = "Item1", Quantity = 2 },
                    new Product { Name = "Item2", Quantity = 2 }
                }
            };

            var handler = new CalculateTrolleyRequestHandler();
            calculateResponse = await handler.Handle(_request, CancellationToken.None);
        }

        [Test]
        public void CalculateResponseShouldNotBeNull()
        {
            Assert.That(calculateResponse, Is.Not.Null);
        }

        [Test]
        public void CalculationShouldBeCorrect()
        {
            Assert.That(calculateResponse, Is.EqualTo(12));
        }
    }
}