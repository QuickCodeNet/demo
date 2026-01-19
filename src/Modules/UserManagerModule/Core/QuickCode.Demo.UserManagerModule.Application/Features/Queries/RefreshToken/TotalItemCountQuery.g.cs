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
    public class TotalCountRefreshTokenQuery : IRequest<Response<int>>
    {
        public TotalCountRefreshTokenQuery()
        {
        }

        public class TotalCountRefreshTokenHandler : IRequestHandler<TotalCountRefreshTokenQuery, Response<int>>
        {
            private readonly ILogger<TotalCountRefreshTokenHandler> _logger;
            private readonly IRefreshTokenRepository _repository;
            public TotalCountRefreshTokenHandler(ILogger<TotalCountRefreshTokenHandler> logger, IRefreshTokenRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(TotalCountRefreshTokenQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}