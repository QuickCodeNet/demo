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
    public class GetRefreshTokensForAspNetUsersQuery : IRequest<Response<List<GetRefreshTokensForAspNetUsersResponseDto>>>
    {
        public string AspNetUsersId { get; set; }

        public GetRefreshTokensForAspNetUsersQuery(string aspNetUsersId)
        {
            this.AspNetUsersId = aspNetUsersId;
        }

        public class GetRefreshTokensForAspNetUsersHandler : IRequestHandler<GetRefreshTokensForAspNetUsersQuery, Response<List<GetRefreshTokensForAspNetUsersResponseDto>>>
        {
            private readonly ILogger<GetRefreshTokensForAspNetUsersHandler> _logger;
            private readonly IAspNetUserRepository _repository;
            public GetRefreshTokensForAspNetUsersHandler(ILogger<GetRefreshTokensForAspNetUsersHandler> logger, IAspNetUserRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetRefreshTokensForAspNetUsersResponseDto>>> Handle(GetRefreshTokensForAspNetUsersQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetRefreshTokensForAspNetUsersAsync(request.AspNetUsersId);
                return returnValue.ToResponse();
            }
        }
    }
}