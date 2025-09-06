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
    public class AspNetUsersGetAspNetUserRolesForAspNetUsersQuery : IRequest<Response<List<AspNetUsersGetAspNetUserRolesForAspNetUsersResponseDto>>>
    {
        public string AspNetUsersId { get; set; }

        public AspNetUsersGetAspNetUserRolesForAspNetUsersQuery(string aspNetUsersId)
        {
            this.AspNetUsersId = aspNetUsersId;
        }

        public class AspNetUsersGetAspNetUserRolesForAspNetUsersHandler : IRequestHandler<AspNetUsersGetAspNetUserRolesForAspNetUsersQuery, Response<List<AspNetUsersGetAspNetUserRolesForAspNetUsersResponseDto>>>
        {
            private readonly ILogger<AspNetUsersGetAspNetUserRolesForAspNetUsersHandler> _logger;
            private readonly IAspNetUsersRepository _repository;
            public AspNetUsersGetAspNetUserRolesForAspNetUsersHandler(ILogger<AspNetUsersGetAspNetUserRolesForAspNetUsersHandler> logger, IAspNetUsersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<AspNetUsersGetAspNetUserRolesForAspNetUsersResponseDto>>> Handle(AspNetUsersGetAspNetUserRolesForAspNetUsersQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.AspNetUsersGetAspNetUserRolesForAspNetUsersAsync(request.AspNetUsersId);
                return returnValue.ToResponse();
            }
        }
    }
}