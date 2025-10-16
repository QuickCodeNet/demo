﻿using System.Linq;
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
    public class DeleteRefreshTokenCommand : IRequest<Response<bool>>
    {
        public RefreshTokenDto request { get; set; }

        public DeleteRefreshTokenCommand(RefreshTokenDto request)
        {
            this.request = request;
        }

        public class DeleteRefreshTokenHandler : IRequestHandler<DeleteRefreshTokenCommand, Response<bool>>
        {
            private readonly ILogger<DeleteRefreshTokenHandler> _logger;
            private readonly IRefreshTokenRepository _repository;
            public DeleteRefreshTokenHandler(ILogger<DeleteRefreshTokenHandler> logger, IRefreshTokenRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeleteRefreshTokenCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}