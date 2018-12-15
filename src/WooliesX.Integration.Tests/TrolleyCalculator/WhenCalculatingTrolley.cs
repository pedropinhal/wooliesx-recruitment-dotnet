using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WooliesX.Domain.Responses;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using WooliesX.Domain.Models;
using WooliesX.Api.Models;
using System.Text;

namespace WooliesX.Integration.Tests
{
    [TestFixture]
    public class WhenCalculatingTrolley : ApiTestBase
    {
        private HttpResponseMessage _response;
        private decimal _view;

        [OneTimeSetUp]
        public async Task Setup()
        {
            var client = Api.CreateClient();

            var model = new CalculateTrolley
            {
                Products = new List<Api.Models.Product>
                {
                    new Api.Models.Product { Name = "Item1", Price = 4 }
                },
                Quantities = new List<Quantity>
                {
                    new Quantity { Name = "Item1",  QuantityValue = 2 }
                },
                Specials = new List<Api.Models.Special>
                {
                    new Api.Models.Special 
                    {
                        Quantities = new List<Quantity>
                        {
                            new Quantity { Name = "Item1",  QuantityValue = 1 }
                        },
                        Total = 3
                    }
                }
            };

            var json = JsonConvert.SerializeObject(model);

            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            _response = await client.PostAsync($"api/trolleyCalculator", stringContent);

            var content = await _response.Content.ReadAsStringAsync();

            _view = JsonConvert.DeserializeObject<decimal>(content);
        }

        [Test]
        public void ShouldReturnFound()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void ShouldReturnCorrectObject()
        {
            Assert.That(_view, Is.Not.Null);
        }

        [Test]
        public void ShouldReturnCorrectSortOrder()
        {
            Assert.That(_view, Is.EqualTo(7));
        }
    }
}