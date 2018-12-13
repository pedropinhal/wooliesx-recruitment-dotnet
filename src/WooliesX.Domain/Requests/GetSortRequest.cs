using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WooliesX.Domain.Responses;
using WooliesX.Domain.Models;
using WooliesX.Domain.Ports;
using System;
using System.Linq;

namespace WooliesX.Domain.Requests
{
    public class GetSortRequest : IRequest<SortResponse>
    {
        public string SortOption { get; set; }
    }

    public class GetSortRequestHandler : IRequestHandler<GetSortRequest, SortResponse>
    {
        private readonly ConfigurationSettings _configurationSettings;
        private readonly IProductsRepository _productsRepository;
        private readonly IShopperHistoryRepository _shopperHistoryRepository;

        public GetSortRequestHandler(IProductsRepository productsRepository,
            IShopperHistoryRepository shopperHistoryRepository)
        {
            _productsRepository = productsRepository;
            _shopperHistoryRepository = shopperHistoryRepository;
        }

        public async Task<SortResponse> Handle(GetSortRequest request, CancellationToken cancellationToken)
        {
            if (request.SortOption.Equals("Recommended", StringComparison.InvariantCultureIgnoreCase))
            {
                var shopperHistory = await _shopperHistoryRepository.GetCustomerHistory();
                var shopperHistoryProducts = shopperHistory.SelectMany(s => s.Products).ToList();

                var popularProducts = shopperHistoryProducts
                    .GroupBy(
                        p => p.Name,
                        p => p.Quantity,
                        (key, group) => new
                        {
                            Name = key,
                            Quantity = group.Sum(),
                            Price = shopperHistoryProducts.Find(l => l.Name == key).Price
                        }
                    )
                    .OrderByDescending(o => o.Quantity)
                    .Select(s => new Product { Name = s.Name, Price = s.Price })
                    .ToList();

                return new SortResponse { Products = popularProducts };
            }

            var products = await _productsRepository.GetProducts();

            switch (request.SortOption)
            {
                case "Low":
                    products.Sort((x, y) => x.Price.CompareTo(y.Price));
                    break;
                case "High":
                    products.Sort((x, y) => y.Price.CompareTo(x.Price));
                    break;
                case "Ascending":
                    products.Sort((x, y) => x.Name.CompareTo(y.Name));
                    break;
                case "Descending":
                    products.Sort((x, y) => y.Name.CompareTo(x.Name));
                    break;
            }

            return new SortResponse { Products = products };
        }
    }
}