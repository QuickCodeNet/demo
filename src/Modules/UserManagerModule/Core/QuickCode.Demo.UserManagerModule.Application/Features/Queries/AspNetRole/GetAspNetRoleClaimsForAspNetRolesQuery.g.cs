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
    public class GetAspNetRoleClaimsForAspNetRolesQuery : IRequest<Response<List<GetAspNetRoleClaimsForAspNetRolesResponseDto>>>
    {
        public string AspNetRolesId { get; set; }

        public GetAspNetRoleClaimsForAspNetRolesQuery(string aspNetRolesId)
        {
            this.AspNetRolesId = aspNetRolesId;
        }

        public class GetAspNetRoleClaimsForAspNetRolesHandler : IRequestHandler<GetAspNetRoleClaimsForAspNetRolesQuery, Response<List<GetAspNetRoleClaimsForAspNetRolesResponseDto>>>
        {
            private readonly ILogger<GetAspNetRoleClaimsForAspNetRolesHandler> _logger;
            private readonly IAspNetRoleRepository _repository;
            public GetAspNetRoleClaimsForAspNetRolesHandler(ILogger<GetAspNetRoleClaimsForAspNetRolesHandler> logger, IAspNetRoleRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetAspNetRoleClaimsForAspNetRolesResponseDto>>> Handle(GetAspNetRoleClaimsForAspNetRolesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetAspNetRoleClaimsForAspNetRolesAsync(request.AspNetRolesId);
                return returnValue.ToResponse();
            }
        }
    }
}