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
    public class AspNetRolesGetAspNetRoleClaimsForAspNetRolesDetailsQuery : IRequest<Response<AspNetRolesGetAspNetRoleClaimsForAspNetRolesResponseDto>>
    {
        public string AspNetRolesId { get; set; }
        public int AspNetRoleClaimsId { get; set; }

        public AspNetRolesGetAspNetRoleClaimsForAspNetRolesDetailsQuery(string aspNetRolesId, int aspNetRoleClaimsId)
        {
            this.AspNetRolesId = aspNetRolesId;
            this.AspNetRoleClaimsId = aspNetRoleClaimsId;
        }

        public class AspNetRolesGetAspNetRoleClaimsForAspNetRolesDetailsHandler : IRequestHandler<AspNetRolesGetAspNetRoleClaimsForAspNetRolesDetailsQuery, Response<AspNetRolesGetAspNetRoleClaimsForAspNetRolesResponseDto>>
        {
            private readonly ILogger<AspNetRolesGetAspNetRoleClaimsForAspNetRolesDetailsHandler> _logger;
            private readonly IAspNetRolesRepository _repository;
            public AspNetRolesGetAspNetRoleClaimsForAspNetRolesDetailsHandler(ILogger<AspNetRolesGetAspNetRoleClaimsForAspNetRolesDetailsHandler> logger, IAspNetRolesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<AspNetRolesGetAspNetRoleClaimsForAspNetRolesResponseDto>> Handle(AspNetRolesGetAspNetRoleClaimsForAspNetRolesDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.AspNetRolesGetAspNetRoleClaimsForAspNetRolesDetailsAsync(request.AspNetRolesId, request.AspNetRoleClaimsId);
                return returnValue.ToResponse();
            }
        }
    }
}