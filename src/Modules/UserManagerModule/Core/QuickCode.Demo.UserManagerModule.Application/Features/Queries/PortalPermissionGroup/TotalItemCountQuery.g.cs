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
using QuickCode.Demo.UserManagerModule.Application.Dtos.PortalPermissionGroup;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.PortalPermissionGroup
{
    public class TotalCountPortalPermissionGroupQuery : IRequest<Response<int>>
    {
        public TotalCountPortalPermissionGroupQuery()
        {
        }

        public class TotalCountPortalPermissionGroupHandler : IRequestHandler<TotalCountPortalPermissionGroupQuery, Response<int>>
        {
            private readonly ILogger<TotalCountPortalPermissionGroupHandler> _logger;
            private readonly IPortalPermissionGroupRepository _repository;
            public TotalCountPortalPermissionGroupHandler(ILogger<TotalCountPortalPermissionGroupHandler> logger, IPortalPermissionGroupRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(TotalCountPortalPermissionGroupQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}