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
    public class AspNetUsersGetRefreshTokensForAspNetUsersQuery : IRequest<Response<List<AspNetUsersGetRefreshTokensForAspNetUsersResponseDto>>>
    {
        public string AspNetUsersId { get; set; }

        public AspNetUsersGetRefreshTokensForAspNetUsersQuery(string aspNetUsersId)
        {
            this.AspNetUsersId = aspNetUsersId;
        }

        public class AspNetUsersGetRefreshTokensForAspNetUsersHandler : IRequestHandler<AspNetUsersGetRefreshTokensForAspNetUsersQuery, Response<List<AspNetUsersGetRefreshTokensForAspNetUsersResponseDto>>>
        {
            private readonly ILogger<AspNetUsersGetRefreshTokensForAspNetUsersHandler> _logger;
            private readonly IAspNetUsersRepository _repository;
            public AspNetUsersGetRefreshTokensForAspNetUsersHandler(ILogger<AspNetUsersGetRefreshTokensForAspNetUsersHandler> logger, IAspNetUsersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<AspNetUsersGetRefreshTokensForAspNetUsersResponseDto>>> Handle(AspNetUsersGetRefreshTokensForAspNetUsersQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.AspNetUsersGetRefreshTokensForAspNetUsersAsync(request.AspNetUsersId);
                return returnValue.ToResponse();
            }
        }
    }
}