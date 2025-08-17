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
    public class PortalPermissionsGetPortalPermissionGroupsForPortalPermissionsDetailsQuery : IRequest<Response<PortalPermissionsGetPortalPermissionGroupsForPortalPermissionsResponseDto>>
    {
        public string PortalPermissionsName { get; set; }
        public string PortalPermissionGroupsPortalPermissionName { get; set; }

        public PortalPermissionsGetPortalPermissionGroupsForPortalPermissionsDetailsQuery(string portalPermissionsName, string portalPermissionGroupsPortalPermissionName)
        {
            this.PortalPermissionsName = portalPermissionsName;
            this.PortalPermissionGroupsPortalPermissionName = portalPermissionGroupsPortalPermissionName;
        }

        public class PortalPermissionsGetPortalPermissionGroupsForPortalPermissionsDetailsHandler : IRequestHandler<PortalPermissionsGetPortalPermissionGroupsForPortalPermissionsDetailsQuery, Response<PortalPermissionsGetPortalPermissionGroupsForPortalPermissionsResponseDto>>
        {
            private readonly ILogger<PortalPermissionsGetPortalPermissionGroupsForPortalPermissionsDetailsHandler> _logger;
            private readonly IPortalPermissionsRepository _repository;
            public PortalPermissionsGetPortalPermissionGroupsForPortalPermissionsDetailsHandler(ILogger<PortalPermissionsGetPortalPermissionGroupsForPortalPermissionsDetailsHandler> logger, IPortalPermissionsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PortalPermissionsGetPortalPermissionGroupsForPortalPermissionsResponseDto>> Handle(PortalPermissionsGetPortalPermissionGroupsForPortalPermissionsDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PortalPermissionsGetPortalPermissionGroupsForPortalPermissionsDetailsAsync(request.PortalPermissionsName, request.PortalPermissionGroupsPortalPermissionName);
                return returnValue.ToResponse();
            }
        }
    }
}