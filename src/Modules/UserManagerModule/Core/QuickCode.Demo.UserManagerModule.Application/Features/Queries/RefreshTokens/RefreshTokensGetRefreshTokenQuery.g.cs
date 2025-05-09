using System;
using System.Linq;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.UserManagerModule.Domain.Entities;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.UserManagerModule.Application.Dtos;

namespace QuickCode.Demo.UserManagerModule.Application.Features
{
    public class RefreshTokensRefreshTokensGetRefreshTokenQuery : IRequest<Response<RefreshTokensGetRefreshTokenResponseDto>>
    {
        public string RefreshTokensToken { get; set; }

        public RefreshTokensRefreshTokensGetRefreshTokenQuery(string refreshTokensToken)
        {
            this.RefreshTokensToken = refreshTokensToken;
        }

        public class RefreshTokensRefreshTokensGetRefreshTokenHandler : IRequestHandler<RefreshTokensRefreshTokensGetRefreshTokenQuery, Response<RefreshTokensGetRefreshTokenResponseDto>>
        {
            private readonly ILogger<RefreshTokensRefreshTokensGetRefreshTokenHandler> _logger;
            private readonly IRefreshTokensRepository _repository;
            public RefreshTokensRefreshTokensGetRefreshTokenHandler(ILogger<RefreshTokensRefreshTokensGetRefreshTokenHandler> logger, IRefreshTokensRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<RefreshTokensGetRefreshTokenResponseDto>> Handle(RefreshTokensRefreshTokensGetRefreshTokenQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.RefreshTokensGetRefreshTokenAsync(request.RefreshTokensToken);
                return returnValue.ToResponse();
            }
        }
    }
}