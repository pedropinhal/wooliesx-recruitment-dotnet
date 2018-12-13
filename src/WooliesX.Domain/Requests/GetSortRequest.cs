using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WooliesX.Domain.Responses;
using WooliesX.Domain.Models;
using WooliesX.Domain.Ports;
using System;

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
                return new SortResponse { Products = shopperHistory.Products };
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