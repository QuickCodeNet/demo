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
    public class AspNetRolesGetAspNetRoleClaimsForAspNetRolesQuery : IRequest<Response<List<AspNetRolesGetAspNetRoleClaimsForAspNetRolesResponseDto>>>
    {
        public string AspNetRolesId { get; set; }

        public AspNetRolesGetAspNetRoleClaimsForAspNetRolesQuery(string aspNetRolesId)
        {
            this.AspNetRolesId = aspNetRolesId;
        }

        public class AspNetRolesGetAspNetRoleClaimsForAspNetRolesHandler : IRequestHandler<AspNetRolesGetAspNetRoleClaimsForAspNetRolesQuery, Response<List<AspNetRolesGetAspNetRoleClaimsForAspNetRolesResponseDto>>>
        {
            private readonly ILogger<AspNetRolesGetAspNetRoleClaimsForAspNetRolesHandler> _logger;
            private readonly IAspNetRolesRepository _repository;
            public AspNetRolesGetAspNetRoleClaimsForAspNetRolesHandler(ILogger<AspNetRolesGetAspNetRoleClaimsForAspNetRolesHandler> logger, IAspNetRolesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<AspNetRolesGetAspNetRoleClaimsForAspNetRolesResponseDto>>> Handle(AspNetRolesGetAspNetRoleClaimsForAspNetRolesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.AspNetRolesGetAspNetRoleClaimsForAspNetRolesAsync(request.AspNetRolesId);
                return returnValue.ToResponse();
            }
        }
    }
}