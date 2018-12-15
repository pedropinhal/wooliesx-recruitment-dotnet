
using System.Collections.Generic;
using System.Linq;
using WooliesX.Domain.Requests;

namespace WooliesX.Api.Models
{
    public static class Mapper
    {
        public static Domain.Models.Product Map(this Product product)
        {
            return new Domain.Models.Product { Name = product.Name, Price = product.Price };
        }

        public static Domain.Models.Product Map(this Quantity quantity)
        {
            return new Domain.Models.Product { Name = quantity.Name, Quantity = quantity.QuantityValue };
        }

        public static Domain.Models.Special Map(this Special special)
        {
            return new Domain.Models.Special
            {
                Total = special.Total,
                Quantities = special.Quantities.Select(q => q.Map()).ToList()
            };
        }

        public static CalculateTrolleyRequest Map(this CalculateTrolley calculateTrolley)
        {
            return new CalculateTrolleyRequest
            {
                Products = calculateTrolley.Products.Select(p => p.Map()).ToList(),
                Quantities = calculateTrolley.Quantities.Select(q => q.Map()).ToList(),
                Specials = calculateTrolley.Specials.Select(s => s.Map()).ToList()
            };
        }


    }
}
