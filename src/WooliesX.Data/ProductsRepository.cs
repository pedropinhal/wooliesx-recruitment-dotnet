using System;
using WooliesX.Domain.Ports;
using System.Threading.Tasks;
using System.Collections.Generic;
using WooliesX.Domain.Models;

namespace WooliesX.Data
{
    public class ProductsRepository : IProductsRepository
    {
        public async Task<List<Product>> GetProducts()
        {
            return new List<Product> { new Product() };
        }
    }
}
