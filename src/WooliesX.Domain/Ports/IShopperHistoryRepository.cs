using System.Collections.Generic;
using System.Threading.Tasks;
using WooliesX.Domain.Models;

namespace WooliesX.Domain.Ports
{
    public interface IShopperHistoryRepository
    {
        Task<ShopperHistory> GetCustomerHistory();
    }
}