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
    public class GetAspNetUserRolesForAspNetUsersQuery : IRequest<Response<List<GetAspNetUserRolesForAspNetUsersResponseDto>>>
    {
        public string AspNetUsersId { get; set; }

        public GetAspNetUserRolesForAspNetUsersQuery(string aspNetUsersId)
        {
            this.AspNetUsersId = aspNetUsersId;
        }

        public class GetAspNetUserRolesForAspNetUsersHandler : IRequestHandler<GetAspNetUserRolesForAspNetUsersQuery, Response<List<GetAspNetUserRolesForAspNetUsersResponseDto>>>
        {
            private readonly ILogger<GetAspNetUserRolesForAspNetUsersHandler> _logger;
            private readonly IAspNetUserRepository _repository;
            public GetAspNetUserRolesForAspNetUsersHandler(ILogger<GetAspNetUserRolesForAspNetUsersHandler> logger, IAspNetUserRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetAspNetUserRolesForAspNetUsersResponseDto>>> Handle(GetAspNetUserRolesForAspNetUsersQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetAspNetUserRolesForAspNetUsersAsync(request.AspNetUsersId);
                return returnValue.ToResponse();
            }
        }
    }
}