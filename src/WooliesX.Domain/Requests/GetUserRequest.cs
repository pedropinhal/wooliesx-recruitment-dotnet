using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WooliesX.Domain.Responses;
using System.Linq;
using WooliesX.Domain.Models;

namespace WooliesX.Domain.Requests
{
    public class GetUserRequest : IRequest<UserResponse>
    {
    }

    public class GetUserRequestHandler : IRequestHandler<GetUserRequest, UserResponse>
    {
        private readonly ConfigurationSettings _configurationSettings;

        public GetUserRequestHandler(ConfigurationSettings configurationSettings)
        {
            _configurationSettings = configurationSettings;
        }

        public async Task<UserResponse> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            return new UserResponse
            {
                Name = _configurationSettings.Name,
                Token = _configurationSettings.Token
            };
        }
    }
}