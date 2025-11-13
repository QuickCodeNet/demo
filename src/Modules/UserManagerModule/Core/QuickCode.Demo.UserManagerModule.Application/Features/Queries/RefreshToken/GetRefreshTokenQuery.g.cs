using System;
using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.UserManagerModule.Domain.Entities;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.UserManagerModule.Application.Dtos.RefreshToken;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.RefreshToken
{
    public class GetRefreshTokenQuery : IRequest<Response<GetRefreshTokenResponseDto>>
    {
        public string RefreshTokensToken { get; set; }

        public GetRefreshTokenQuery(string refreshTokensToken)
        {
            this.RefreshTokensToken = refreshTokensToken;
        }

        public class GetRefreshTokenHandler : IRequestHandler<GetRefreshTokenQuery, Response<GetRefreshTokenResponseDto>>
        {
            private readonly ILogger<GetRefreshTokenHandler> _logger;
            private readonly IRefreshTokenRepository _repository;
            public GetRefreshTokenHandler(ILogger<GetRefreshTokenHandler> logger, IRefreshTokenRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<GetRefreshTokenResponseDto>> Handle(GetRefreshTokenQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetRefreshTokenAsync(request.RefreshTokensToken);
                return returnValue.ToResponse();
            }
        }
    }
}