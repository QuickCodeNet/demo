using System;
using System.Linq;
using MediatR;
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
    public class AspNetRolesAspNetRolesAspNetRoleClaims_KEY_RESTQuery : IRequest<Response<AspNetRolesAspNetRoleClaims_KEY_RESTResponseDto>>
    {
        public string AspNetRolesId { get; set; }
        public int AspNetRoleClaimsId { get; set; }

        public AspNetRolesAspNetRolesAspNetRoleClaims_KEY_RESTQuery(string aspNetRolesId, int aspNetRoleClaimsId)
        {
            this.AspNetRolesId = aspNetRolesId;
            this.AspNetRoleClaimsId = aspNetRoleClaimsId;
        }

        public class AspNetRolesAspNetRolesAspNetRoleClaims_KEY_RESTHandler : IRequestHandler<AspNetRolesAspNetRolesAspNetRoleClaims_KEY_RESTQuery, Response<AspNetRolesAspNetRoleClaims_KEY_RESTResponseDto>>
        {
            private readonly ILogger<AspNetRolesAspNetRolesAspNetRoleClaims_KEY_RESTHandler> _logger;
            private readonly IAspNetRolesRepository _repository;
            public AspNetRolesAspNetRolesAspNetRoleClaims_KEY_RESTHandler(ILogger<AspNetRolesAspNetRolesAspNetRoleClaims_KEY_RESTHandler> logger, IAspNetRolesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<AspNetRolesAspNetRoleClaims_KEY_RESTResponseDto>> Handle(AspNetRolesAspNetRolesAspNetRoleClaims_KEY_RESTQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.AspNetRolesAspNetRoleClaims_KEY_RESTAsync(request.AspNetRolesId, request.AspNetRoleClaimsId);
                return returnValue.ToResponse();
            }
        }
    }
}