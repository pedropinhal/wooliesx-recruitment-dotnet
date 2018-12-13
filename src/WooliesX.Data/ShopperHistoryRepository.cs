using System;
using WooliesX.Domain.Ports;
using System.Threading.Tasks;
using System.Collections.Generic;
using WooliesX.Domain.Models;

namespace WooliesX.Data
{
    public class ShopperHistoryRepository : IShopperHistoryRepository
    {
        private readonly IHttpClient _httpClient;
        private readonly ConfigurationSettings _configurationSettings;

        public ShopperHistoryRepository(IHttpClient httpClient, ConfigurationSettings configurationSettings)
        {
            _httpClient = httpClient;
            _configurationSettings = configurationSettings;
        }

        public async Task<ShopperHistory> GetCustomerHistory()
        {
            var url = new Uri($"{_configurationSettings.ApiHost}/api/resource/shopperHistory?token={_configurationSettings.Token}");
            var shopperHistory = await _httpClient.GetAsync<ShopperHistory>(url);
            return shopperHistory;
        }
    }
}
