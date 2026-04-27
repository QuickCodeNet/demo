using System;
using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.IdentityModule.Domain.Entities;
using QuickCode.Demo.IdentityModule.Application.Interfaces.Repositories;
using QuickCode.Demo.IdentityModule.Application.Dtos.RefreshToken;
using QuickCode.Demo.IdentityModule.Domain.Enums;

namespace QuickCode.Demo.IdentityModule.Application.Features.RefreshToken
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