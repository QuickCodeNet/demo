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
using QuickCode.Demo.UserManagerModule.Application.Dtos;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features
{
    public class RefreshTokensGetRefreshTokenQuery : IRequest<Response<RefreshTokensGetRefreshTokenResponseDto>>
    {
        public string RefreshTokensToken { get; set; }

        public RefreshTokensGetRefreshTokenQuery(string refreshTokensToken)
        {
            this.RefreshTokensToken = refreshTokensToken;
        }

        public class RefreshTokensGetRefreshTokenHandler : IRequestHandler<RefreshTokensGetRefreshTokenQuery, Response<RefreshTokensGetRefreshTokenResponseDto>>
        {
            private readonly ILogger<RefreshTokensGetRefreshTokenHandler> _logger;
            private readonly IRefreshTokensRepository _repository;
            public RefreshTokensGetRefreshTokenHandler(ILogger<RefreshTokensGetRefreshTokenHandler> logger, IRefreshTokensRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<RefreshTokensGetRefreshTokenResponseDto>> Handle(RefreshTokensGetRefreshTokenQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.RefreshTokensGetRefreshTokenAsync(request.RefreshTokensToken);
                return returnValue.ToResponse();
            }
        }
    }
}