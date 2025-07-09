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

namespace QuickCode.Demo.UserManagerModule.Application.Features
{
    public class AspNetUsersGetAspNetUserTokensForAspNetUsersDetailsQuery : IRequest<Response<AspNetUsersGetAspNetUserTokensForAspNetUsersResponseDto>>
    {
        public string AspNetUsersId { get; set; }
        public string AspNetUserTokensUserId { get; set; }

        public AspNetUsersGetAspNetUserTokensForAspNetUsersDetailsQuery(string aspNetUsersId, string aspNetUserTokensUserId)
        {
            this.AspNetUsersId = aspNetUsersId;
            this.AspNetUserTokensUserId = aspNetUserTokensUserId;
        }

        public class AspNetUsersGetAspNetUserTokensForAspNetUsersDetailsHandler : IRequestHandler<AspNetUsersGetAspNetUserTokensForAspNetUsersDetailsQuery, Response<AspNetUsersGetAspNetUserTokensForAspNetUsersResponseDto>>
        {
            private readonly ILogger<AspNetUsersGetAspNetUserTokensForAspNetUsersDetailsHandler> _logger;
            private readonly IAspNetUsersRepository _repository;
            public AspNetUsersGetAspNetUserTokensForAspNetUsersDetailsHandler(ILogger<AspNetUsersGetAspNetUserTokensForAspNetUsersDetailsHandler> logger, IAspNetUsersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<AspNetUsersGetAspNetUserTokensForAspNetUsersResponseDto>> Handle(AspNetUsersGetAspNetUserTokensForAspNetUsersDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.AspNetUsersGetAspNetUserTokensForAspNetUsersDetailsAsync(request.AspNetUsersId, request.AspNetUserTokensUserId);
                return returnValue.ToResponse();
            }
        }
    }
}