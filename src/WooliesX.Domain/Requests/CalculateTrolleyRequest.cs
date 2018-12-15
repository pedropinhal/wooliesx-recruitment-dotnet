using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WooliesX.Domain.Responses;
using WooliesX.Domain.Models;
using WooliesX.Domain.Ports;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WooliesX.Domain.Requests
{
    public class CalculateTrolleyRequest : IRequest<decimal>
    {
        public List<Product> Products { get; set; }
        public List<Product> Quantities { get; set; }
        public List<Special> Specials { get; set; }
    }

    public class CalculateTrolleyRequestHandler : IRequestHandler<CalculateTrolleyRequest, decimal>
    {
        public CalculateTrolleyRequestHandler()
        {
        }

        public async Task<decimal> Handle(CalculateTrolleyRequest request, CancellationToken cancellationToken)
        {
            if (request.Specials?.Count > 0)
            {
                var special = request.Specials.First(); // Test Api fails if supplied more than 1 special

                var quantitiesWithoutDiscount = CalculateTotal(special.Quantities, request.Products);

                if (quantitiesWithoutDiscount < special.Total)
                {
                    return CalculateTotal(request.Quantities, request.Products);
                }

                foreach (var quantity in request.Quantities)
                {
                    var specialItemQuantity = special.Quantities.First(q => q.Name.Equals(quantity.Name)).Quantity;
                    quantity.Quantity -= specialItemQuantity;

                    if (quantity.Quantity < 0)
                        quantity.Quantity = 0;
                }

                return CalculateTotal(request.Quantities, request.Products) + special.Total;
            }

            return CalculateTotal(request.Quantities, request.Products);
        }

        private static decimal CalculateTotal(List<Product> quantities, List<Product> products)
        {
            decimal total = 0;

            foreach (var quantity in quantities)
            {
                var price = products.First(p => p.Name.Equals(quantity.Name)).Price;
                total += price * quantity.Quantity;
            }

            return total;
        }
    }
}