using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WooliesX.Data
{
    public interface IHttpClient
    {
        Task<T> GetAsync<T>(Uri endpoint);
    }

    public class HttpClient : IHttpClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<T> GetAsync<T>(Uri endpoint)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetStringAsync(endpoint);
            
            var model = JsonConvert.DeserializeObject<T>(response);
            return model;
        }
    }
}