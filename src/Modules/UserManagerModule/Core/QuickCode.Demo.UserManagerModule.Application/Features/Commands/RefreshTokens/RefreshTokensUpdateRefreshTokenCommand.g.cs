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
    public class RefreshTokensUpdateRefreshTokenCommand : IRequest<Response<int>>
    {
        public string RefreshTokensToken { get; set; }
        public RefreshTokensUpdateRefreshTokenRequestDto UpdateRequest { get; set; }

        public RefreshTokensUpdateRefreshTokenCommand(string refreshTokensToken, RefreshTokensUpdateRefreshTokenRequestDto updateRequest)
        {
            this.RefreshTokensToken = refreshTokensToken;
            this.UpdateRequest = updateRequest;
        }

        public class RefreshTokensUpdateRefreshTokenHandler : IRequestHandler<RefreshTokensUpdateRefreshTokenCommand, Response<int>>
        {
            private readonly ILogger<RefreshTokensUpdateRefreshTokenHandler> _logger;
            private readonly IRefreshTokensRepository _repository;
            public RefreshTokensUpdateRefreshTokenHandler(ILogger<RefreshTokensUpdateRefreshTokenHandler> logger, IRefreshTokensRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(RefreshTokensUpdateRefreshTokenCommand request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.RefreshTokensUpdateRefreshTokenAsync(request.RefreshTokensToken, request.UpdateRequest);
                return returnValue.ToResponse();
            }
        }
    }
}