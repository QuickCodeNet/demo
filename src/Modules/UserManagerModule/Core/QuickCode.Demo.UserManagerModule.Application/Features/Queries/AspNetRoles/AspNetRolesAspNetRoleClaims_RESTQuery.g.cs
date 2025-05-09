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
    public class AspNetRolesAspNetRolesAspNetRoleClaims_RESTQuery : IRequest<Response<List<AspNetRolesAspNetRoleClaims_RESTResponseDto>>>
    {
        public string AspNetRolesId { get; set; }

        public AspNetRolesAspNetRolesAspNetRoleClaims_RESTQuery(string aspNetRolesId)
        {
            this.AspNetRolesId = aspNetRolesId;
        }

        public class AspNetRolesAspNetRolesAspNetRoleClaims_RESTHandler : IRequestHandler<AspNetRolesAspNetRolesAspNetRoleClaims_RESTQuery, Response<List<AspNetRolesAspNetRoleClaims_RESTResponseDto>>>
        {
            private readonly ILogger<AspNetRolesAspNetRolesAspNetRoleClaims_RESTHandler> _logger;
            private readonly IAspNetRolesRepository _repository;
            public AspNetRolesAspNetRolesAspNetRoleClaims_RESTHandler(ILogger<AspNetRolesAspNetRolesAspNetRoleClaims_RESTHandler> logger, IAspNetRolesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<AspNetRolesAspNetRoleClaims_RESTResponseDto>>> Handle(AspNetRolesAspNetRolesAspNetRoleClaims_RESTQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.AspNetRolesAspNetRoleClaims_RESTAsync(request.AspNetRolesId);
                return returnValue.ToResponse();
            }
        }
    }
}