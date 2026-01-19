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
using QuickCode.Demo.UserManagerModule.Application.Dtos.AspNetUser;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.AspNetUser
{
    public class GetRefreshTokensForAspNetUsersDetailsQuery : IRequest<Response<GetRefreshTokensForAspNetUsersResponseDto>>
    {
        public string AspNetUsersId { get; set; }
        public int RefreshTokensId { get; set; }

        public GetRefreshTokensForAspNetUsersDetailsQuery(string aspNetUsersId, int refreshTokensId)
        {
            this.AspNetUsersId = aspNetUsersId;
            this.RefreshTokensId = refreshTokensId;
        }

        public class GetRefreshTokensForAspNetUsersDetailsHandler : IRequestHandler<GetRefreshTokensForAspNetUsersDetailsQuery, Response<GetRefreshTokensForAspNetUsersResponseDto>>
        {
            private readonly ILogger<GetRefreshTokensForAspNetUsersDetailsHandler> _logger;
            private readonly IAspNetUserRepository _repository;
            public GetRefreshTokensForAspNetUsersDetailsHandler(ILogger<GetRefreshTokensForAspNetUsersDetailsHandler> logger, IAspNetUserRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<GetRefreshTokensForAspNetUsersResponseDto>> Handle(GetRefreshTokensForAspNetUsersDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetRefreshTokensForAspNetUsersDetailsAsync(request.AspNetUsersId, request.RefreshTokensId);
                return returnValue.ToResponse();
            }
        }
    }
}