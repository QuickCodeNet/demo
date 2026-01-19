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
using QuickCode.Demo.UserManagerModule.Application.Dtos.AspNetUserRole;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.AspNetUserRole
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