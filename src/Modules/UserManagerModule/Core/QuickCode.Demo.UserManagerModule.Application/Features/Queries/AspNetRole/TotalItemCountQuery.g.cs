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
    public class TotalCountAspNetRoleQuery : IRequest<Response<int>>
    {
        public TotalCountAspNetRoleQuery()
        {
        }

        public class TotalCountAspNetRoleHandler : IRequestHandler<TotalCountAspNetRoleQuery, Response<int>>
        {
            private readonly ILogger<TotalCountAspNetRoleHandler> _logger;
            private readonly IAspNetRoleRepository _repository;
            public TotalCountAspNetRoleHandler(ILogger<TotalCountAspNetRoleHandler> logger, IAspNetRoleRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(TotalCountAspNetRoleQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}