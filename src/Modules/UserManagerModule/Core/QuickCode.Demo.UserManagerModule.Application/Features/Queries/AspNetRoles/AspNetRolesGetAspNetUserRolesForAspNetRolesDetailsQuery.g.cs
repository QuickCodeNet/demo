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
    public class AspNetRolesGetAspNetUserRolesForAspNetRolesDetailsQuery : IRequest<Response<AspNetRolesGetAspNetUserRolesForAspNetRolesResponseDto>>
    {
        public string AspNetRolesId { get; set; }
        public string AspNetUserRolesUserId { get; set; }

        public AspNetRolesGetAspNetUserRolesForAspNetRolesDetailsQuery(string aspNetRolesId, string aspNetUserRolesUserId)
        {
            this.AspNetRolesId = aspNetRolesId;
            this.AspNetUserRolesUserId = aspNetUserRolesUserId;
        }

        public class AspNetRolesGetAspNetUserRolesForAspNetRolesDetailsHandler : IRequestHandler<AspNetRolesGetAspNetUserRolesForAspNetRolesDetailsQuery, Response<AspNetRolesGetAspNetUserRolesForAspNetRolesResponseDto>>
        {
            private readonly ILogger<AspNetRolesGetAspNetUserRolesForAspNetRolesDetailsHandler> _logger;
            private readonly IAspNetRolesRepository _repository;
            public AspNetRolesGetAspNetUserRolesForAspNetRolesDetailsHandler(ILogger<AspNetRolesGetAspNetUserRolesForAspNetRolesDetailsHandler> logger, IAspNetRolesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<AspNetRolesGetAspNetUserRolesForAspNetRolesResponseDto>> Handle(AspNetRolesGetAspNetUserRolesForAspNetRolesDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.AspNetRolesGetAspNetUserRolesForAspNetRolesDetailsAsync(request.AspNetRolesId, request.AspNetUserRolesUserId);
                return returnValue.ToResponse();
            }
        }
    }
}