using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WooliesX.Domain.Responses;
using Newtonsoft.Json;
using NUnit.Framework;

namespace WooliesX.Integration.Tests
{
    [TestFixture]
    public class WhenSorting : ApiTestBase
    {
        private HttpResponseMessage _response;
        private SortResponse _view;

        [OneTimeSetUp]
        public async Task Setup()
        {
            _response = await Api
                .CreateRequest($"api/sort?sortOption=Low")
                .GetAsync();

            var content = await _response.Content.ReadAsStringAsync();
            _view = JsonConvert.DeserializeObject<SortResponse>(content);
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
            Assert.That(_view.Products.Count, Is.GreaterThan(0));
        }
    }
}