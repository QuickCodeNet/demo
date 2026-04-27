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
    public class UpdateRefreshTokensCommand : IRequest<Response<int>>
    {
        public string RefreshTokenToken { get; set; }
        public UpdateRefreshTokensRequestDto UpdateRequest { get; set; }

        public UpdateRefreshTokensCommand(string refreshTokenToken, UpdateRefreshTokensRequestDto updateRequest)
        {
            this.RefreshTokenToken = refreshTokenToken;
            this.UpdateRequest = updateRequest;
        }

        public class UpdateRefreshTokensHandler : IRequestHandler<UpdateRefreshTokensCommand, Response<int>>
        {
            private readonly ILogger<UpdateRefreshTokensHandler> _logger;
            private readonly IRefreshTokenRepository _repository;
            public UpdateRefreshTokensHandler(ILogger<UpdateRefreshTokensHandler> logger, IRefreshTokenRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(UpdateRefreshTokensCommand request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.UpdateRefreshTokensAsync(request.RefreshTokenToken, request.UpdateRequest);
                return returnValue.ToResponse();
            }
        }
    }
}