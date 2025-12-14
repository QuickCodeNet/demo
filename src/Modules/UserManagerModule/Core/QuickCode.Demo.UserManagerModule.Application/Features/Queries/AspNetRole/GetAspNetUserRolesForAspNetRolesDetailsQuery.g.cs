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
using QuickCode.Demo.UserManagerModule.Application.Dtos.AspNetRole;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.AspNetRole
{
    public class GetAspNetUserRolesForAspNetRolesDetailsQuery : IRequest<Response<GetAspNetUserRolesForAspNetRolesResponseDto>>
    {
        public string AspNetRolesId { get; set; }
        public string AspNetUserRolesUserId { get; set; }

        public GetAspNetUserRolesForAspNetRolesDetailsQuery(string aspNetRolesId, string aspNetUserRolesUserId)
        {
            this.AspNetRolesId = aspNetRolesId;
            this.AspNetUserRolesUserId = aspNetUserRolesUserId;
        }

        public class GetAspNetUserRolesForAspNetRolesDetailsHandler : IRequestHandler<GetAspNetUserRolesForAspNetRolesDetailsQuery, Response<GetAspNetUserRolesForAspNetRolesResponseDto>>
        {
            private readonly ILogger<GetAspNetUserRolesForAspNetRolesDetailsHandler> _logger;
            private readonly IAspNetRoleRepository _repository;
            public GetAspNetUserRolesForAspNetRolesDetailsHandler(ILogger<GetAspNetUserRolesForAspNetRolesDetailsHandler> logger, IAspNetRoleRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<GetAspNetUserRolesForAspNetRolesResponseDto>> Handle(GetAspNetUserRolesForAspNetRolesDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetAspNetUserRolesForAspNetRolesDetailsAsync(request.AspNetRolesId, request.AspNetUserRolesUserId);
                return returnValue.ToResponse();
            }
        }
    }
}