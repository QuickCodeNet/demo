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
    public class AspNetRolesGetAspNetUserRolesForAspNetRolesQuery : IRequest<Response<List<AspNetRolesGetAspNetUserRolesForAspNetRolesResponseDto>>>
    {
        public string AspNetRolesId { get; set; }

        public AspNetRolesGetAspNetUserRolesForAspNetRolesQuery(string aspNetRolesId)
        {
            this.AspNetRolesId = aspNetRolesId;
        }

        public class AspNetRolesGetAspNetUserRolesForAspNetRolesHandler : IRequestHandler<AspNetRolesGetAspNetUserRolesForAspNetRolesQuery, Response<List<AspNetRolesGetAspNetUserRolesForAspNetRolesResponseDto>>>
        {
            private readonly ILogger<AspNetRolesGetAspNetUserRolesForAspNetRolesHandler> _logger;
            private readonly IAspNetRolesRepository _repository;
            public AspNetRolesGetAspNetUserRolesForAspNetRolesHandler(ILogger<AspNetRolesGetAspNetUserRolesForAspNetRolesHandler> logger, IAspNetRolesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<AspNetRolesGetAspNetUserRolesForAspNetRolesResponseDto>>> Handle(AspNetRolesGetAspNetUserRolesForAspNetRolesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.AspNetRolesGetAspNetUserRolesForAspNetRolesAsync(request.AspNetRolesId);
                return returnValue.ToResponse();
            }
        }
    }
}