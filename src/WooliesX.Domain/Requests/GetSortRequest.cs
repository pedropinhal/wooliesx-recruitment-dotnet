using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WooliesX.Domain.Responses;
using System.Linq;
using WooliesX.Domain.Models;
using System.Collections.Generic;
using WooliesX.Domain.Ports;

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

        public GetSortRequestHandler(ConfigurationSettings configurationSettings, IProductsRepository productsRepository)
        {
            _configurationSettings = configurationSettings;
            _productsRepository = productsRepository;
        }

        public async Task<SortResponse> Handle(GetSortRequest request, CancellationToken cancellationToken)
        {
            var products = await _productsRepository.GetProducts();
            return new SortResponse { Products = products };
        }
    }
}