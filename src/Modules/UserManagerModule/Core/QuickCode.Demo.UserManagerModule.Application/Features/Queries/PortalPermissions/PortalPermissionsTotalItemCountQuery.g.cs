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
    public class PortalPermissionsTotalItemCountQuery : IRequest<Response<int>>
    {
        public PortalPermissionsTotalItemCountQuery()
        {
        }

        public class PortalPermissionsTotalItemCountHandler : IRequestHandler<PortalPermissionsTotalItemCountQuery, Response<int>>
        {
            private readonly ILogger<PortalPermissionsTotalItemCountHandler> _logger;
            private readonly IPortalPermissionsRepository _repository;
            public PortalPermissionsTotalItemCountHandler(ILogger<PortalPermissionsTotalItemCountHandler> logger, IPortalPermissionsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(PortalPermissionsTotalItemCountQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}