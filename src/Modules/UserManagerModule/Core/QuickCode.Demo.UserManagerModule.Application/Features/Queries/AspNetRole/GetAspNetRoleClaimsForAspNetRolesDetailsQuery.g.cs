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
    public class GetAspNetRoleClaimsForAspNetRolesDetailsQuery : IRequest<Response<GetAspNetRoleClaimsForAspNetRolesResponseDto>>
    {
        public string AspNetRolesId { get; set; }
        public int AspNetRoleClaimsId { get; set; }

        public GetAspNetRoleClaimsForAspNetRolesDetailsQuery(string aspNetRolesId, int aspNetRoleClaimsId)
        {
            this.AspNetRolesId = aspNetRolesId;
            this.AspNetRoleClaimsId = aspNetRoleClaimsId;
        }

        public class GetAspNetRoleClaimsForAspNetRolesDetailsHandler : IRequestHandler<GetAspNetRoleClaimsForAspNetRolesDetailsQuery, Response<GetAspNetRoleClaimsForAspNetRolesResponseDto>>
        {
            private readonly ILogger<GetAspNetRoleClaimsForAspNetRolesDetailsHandler> _logger;
            private readonly IAspNetRoleRepository _repository;
            public GetAspNetRoleClaimsForAspNetRolesDetailsHandler(ILogger<GetAspNetRoleClaimsForAspNetRolesDetailsHandler> logger, IAspNetRoleRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<GetAspNetRoleClaimsForAspNetRolesResponseDto>> Handle(GetAspNetRoleClaimsForAspNetRolesDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetAspNetRoleClaimsForAspNetRolesDetailsAsync(request.AspNetRolesId, request.AspNetRoleClaimsId);
                return returnValue.ToResponse();
            }
        }
    }
}