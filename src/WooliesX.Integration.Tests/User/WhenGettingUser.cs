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
    public class WhenGettingUser : ApiTestBase
    {
        private HttpResponseMessage _response;
        private UserResponse _view;
        private string _name = "name";
        private string _token = "token";


        [OneTimeSetUp]
        public async Task Setup()
        {
            _response = await Api
                .CreateRequest($"api/user")
                .GetAsync();

            var content = await _response.Content.ReadAsStringAsync();
            _view = JsonConvert.DeserializeObject<UserResponse>(content);
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
        public void ShouldReturnCorrectUser()
        {
            Assert.That(_view.Name, Is.EqualTo(_name));
            Assert.That(_view.Token, Is.EqualTo(_token));
        }
    }
}