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
    public class AspNetUsersGetRefreshTokensForAspNetUsersDetailsQuery : IRequest<Response<AspNetUsersGetRefreshTokensForAspNetUsersResponseDto>>
    {
        public string AspNetUsersId { get; set; }
        public int RefreshTokensId { get; set; }

        public AspNetUsersGetRefreshTokensForAspNetUsersDetailsQuery(string aspNetUsersId, int refreshTokensId)
        {
            this.AspNetUsersId = aspNetUsersId;
            this.RefreshTokensId = refreshTokensId;
        }

        public class AspNetUsersGetRefreshTokensForAspNetUsersDetailsHandler : IRequestHandler<AspNetUsersGetRefreshTokensForAspNetUsersDetailsQuery, Response<AspNetUsersGetRefreshTokensForAspNetUsersResponseDto>>
        {
            private readonly ILogger<AspNetUsersGetRefreshTokensForAspNetUsersDetailsHandler> _logger;
            private readonly IAspNetUsersRepository _repository;
            public AspNetUsersGetRefreshTokensForAspNetUsersDetailsHandler(ILogger<AspNetUsersGetRefreshTokensForAspNetUsersDetailsHandler> logger, IAspNetUsersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<AspNetUsersGetRefreshTokensForAspNetUsersResponseDto>> Handle(AspNetUsersGetRefreshTokensForAspNetUsersDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.AspNetUsersGetRefreshTokensForAspNetUsersDetailsAsync(request.AspNetUsersId, request.RefreshTokensId);
                return returnValue.ToResponse();
            }
        }
    }
}