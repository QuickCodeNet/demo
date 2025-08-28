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
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features
{
    public class PortalPermissionsGetPortalPermissionGroupsForPortalPermissionsQuery : IRequest<Response<List<PortalPermissionsGetPortalPermissionGroupsForPortalPermissionsResponseDto>>>
    {
        public string PortalPermissionsName { get; set; }

        public PortalPermissionsGetPortalPermissionGroupsForPortalPermissionsQuery(string portalPermissionsName)
        {
            this.PortalPermissionsName = portalPermissionsName;
        }

        public class PortalPermissionsGetPortalPermissionGroupsForPortalPermissionsHandler : IRequestHandler<PortalPermissionsGetPortalPermissionGroupsForPortalPermissionsQuery, Response<List<PortalPermissionsGetPortalPermissionGroupsForPortalPermissionsResponseDto>>>
        {
            private readonly ILogger<PortalPermissionsGetPortalPermissionGroupsForPortalPermissionsHandler> _logger;
            private readonly IPortalPermissionsRepository _repository;
            public PortalPermissionsGetPortalPermissionGroupsForPortalPermissionsHandler(ILogger<PortalPermissionsGetPortalPermissionGroupsForPortalPermissionsHandler> logger, IPortalPermissionsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<PortalPermissionsGetPortalPermissionGroupsForPortalPermissionsResponseDto>>> Handle(PortalPermissionsGetPortalPermissionGroupsForPortalPermissionsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PortalPermissionsGetPortalPermissionGroupsForPortalPermissionsAsync(request.PortalPermissionsName);
                return returnValue.ToResponse();
            }
        }
    }
}