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
    public class AspNetUsersGetAspNetUserRolesForAspNetUsersDetailsQuery : IRequest<Response<AspNetUsersGetAspNetUserRolesForAspNetUsersResponseDto>>
    {
        public string AspNetUsersId { get; set; }
        public string AspNetUserRolesUserId { get; set; }

        public AspNetUsersGetAspNetUserRolesForAspNetUsersDetailsQuery(string aspNetUsersId, string aspNetUserRolesUserId)
        {
            this.AspNetUsersId = aspNetUsersId;
            this.AspNetUserRolesUserId = aspNetUserRolesUserId;
        }

        public class AspNetUsersGetAspNetUserRolesForAspNetUsersDetailsHandler : IRequestHandler<AspNetUsersGetAspNetUserRolesForAspNetUsersDetailsQuery, Response<AspNetUsersGetAspNetUserRolesForAspNetUsersResponseDto>>
        {
            private readonly ILogger<AspNetUsersGetAspNetUserRolesForAspNetUsersDetailsHandler> _logger;
            private readonly IAspNetUsersRepository _repository;
            public AspNetUsersGetAspNetUserRolesForAspNetUsersDetailsHandler(ILogger<AspNetUsersGetAspNetUserRolesForAspNetUsersDetailsHandler> logger, IAspNetUsersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<AspNetUsersGetAspNetUserRolesForAspNetUsersResponseDto>> Handle(AspNetUsersGetAspNetUserRolesForAspNetUsersDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.AspNetUsersGetAspNetUserRolesForAspNetUsersDetailsAsync(request.AspNetUsersId, request.AspNetUserRolesUserId);
                return returnValue.ToResponse();
            }
        }
    }
}