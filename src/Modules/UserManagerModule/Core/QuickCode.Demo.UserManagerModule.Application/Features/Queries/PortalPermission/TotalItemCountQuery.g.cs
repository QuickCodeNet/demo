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
using QuickCode.Demo.UserManagerModule.Application.Dtos.PortalPermission;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.PortalPermission
{
    public class TotalCountPortalPermissionQuery : IRequest<Response<int>>
    {
        public TotalCountPortalPermissionQuery()
        {
        }

        public class TotalCountPortalPermissionHandler : IRequestHandler<TotalCountPortalPermissionQuery, Response<int>>
        {
            private readonly ILogger<TotalCountPortalPermissionHandler> _logger;
            private readonly IPortalPermissionRepository _repository;
            public TotalCountPortalPermissionHandler(ILogger<TotalCountPortalPermissionHandler> logger, IPortalPermissionRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(TotalCountPortalPermissionQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}