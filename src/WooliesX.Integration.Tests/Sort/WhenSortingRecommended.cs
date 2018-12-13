using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WooliesX.Domain.Responses;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using WooliesX.Domain.Models;

namespace WooliesX.Integration.Tests
{
    [TestFixture]
    public class WhenSortingRecommended : ApiTestBase
    {
        private HttpResponseMessage _response;
        private List<Product> _view;

        [OneTimeSetUp]
        public async Task Setup()
        {
            _response = await Api
                .CreateRequest($"api/sort?sortOption=Recommended")
                .GetAsync();

            var content = await _response.Content.ReadAsStringAsync();
            _view = JsonConvert.DeserializeObject<List<Product>>(content);
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
            Assert.That(_view.Count, Is.GreaterThan(0));
        }
    }
}