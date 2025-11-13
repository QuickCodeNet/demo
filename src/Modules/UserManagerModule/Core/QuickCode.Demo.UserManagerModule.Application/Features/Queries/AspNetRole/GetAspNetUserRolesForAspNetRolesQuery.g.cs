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
    public class GetAspNetUserRolesForAspNetRolesQuery : IRequest<Response<List<GetAspNetUserRolesForAspNetRolesResponseDto>>>
    {
        public string AspNetRolesId { get; set; }

        public GetAspNetUserRolesForAspNetRolesQuery(string aspNetRolesId)
        {
            this.AspNetRolesId = aspNetRolesId;
        }

        public class GetAspNetUserRolesForAspNetRolesHandler : IRequestHandler<GetAspNetUserRolesForAspNetRolesQuery, Response<List<GetAspNetUserRolesForAspNetRolesResponseDto>>>
        {
            private readonly ILogger<GetAspNetUserRolesForAspNetRolesHandler> _logger;
            private readonly IAspNetRoleRepository _repository;
            public GetAspNetUserRolesForAspNetRolesHandler(ILogger<GetAspNetUserRolesForAspNetRolesHandler> logger, IAspNetRoleRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetAspNetUserRolesForAspNetRolesResponseDto>>> Handle(GetAspNetUserRolesForAspNetRolesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetAspNetUserRolesForAspNetRolesAsync(request.AspNetRolesId);
                return returnValue.ToResponse();
            }
        }
    }
}