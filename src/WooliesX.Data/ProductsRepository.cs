using System;
using WooliesX.Domain.Ports;
using System.Threading.Tasks;
using System.Collections.Generic;
using WooliesX.Domain.Models;

namespace WooliesX.Data
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly IHttpClient _httpClient;
        private readonly ConfigurationSettings _configurationSettings;

        public ProductsRepository(IHttpClient httpClient, ConfigurationSettings configurationSettings)
        {
            _httpClient = httpClient;
            _configurationSettings = configurationSettings;
        }

        public async Task<List<Product>> GetProducts()
        {
            var url = new Uri($"{_configurationSettings.ApiHost}/api/resource/products?token={_configurationSettings.Token}");
            var products = await _httpClient.GetAsync<List<Product>>(url);
            return products;
        }
    }
}
