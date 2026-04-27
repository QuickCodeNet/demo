using System;
using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.IdentityModule.Domain.Entities;
using QuickCode.Demo.IdentityModule.Application.Interfaces.Repositories;
using QuickCode.Demo.IdentityModule.Application.Dtos.AspNetUserRole;
using QuickCode.Demo.IdentityModule.Domain.Enums;

namespace QuickCode.Demo.IdentityModule.Application.Features.AspNetUserRole
{
    public class TotalCountAspNetUserRoleQuery : IRequest<Response<int>>
    {
        public TotalCountAspNetUserRoleQuery()
        {
        }

        public class TotalCountAspNetUserRoleHandler : IRequestHandler<TotalCountAspNetUserRoleQuery, Response<int>>
        {
            private readonly ILogger<TotalCountAspNetUserRoleHandler> _logger;
            private readonly IAspNetUserRoleRepository _repository;
            public TotalCountAspNetUserRoleHandler(ILogger<TotalCountAspNetUserRoleHandler> logger, IAspNetUserRoleRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(TotalCountAspNetUserRoleQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}