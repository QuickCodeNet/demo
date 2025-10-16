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
    public class GetPortalPermissionGroupsForPortalPermissionsDetailsQuery : IRequest<Response<GetPortalPermissionGroupsForPortalPermissionsResponseDto>>
    {
        public string PortalPermissionsName { get; set; }
        public string PortalPermissionGroupsPortalPermissionName { get; set; }

        public GetPortalPermissionGroupsForPortalPermissionsDetailsQuery(string portalPermissionsName, string portalPermissionGroupsPortalPermissionName)
        {
            this.PortalPermissionsName = portalPermissionsName;
            this.PortalPermissionGroupsPortalPermissionName = portalPermissionGroupsPortalPermissionName;
        }

        public class GetPortalPermissionGroupsForPortalPermissionsDetailsHandler : IRequestHandler<GetPortalPermissionGroupsForPortalPermissionsDetailsQuery, Response<GetPortalPermissionGroupsForPortalPermissionsResponseDto>>
        {
            private readonly ILogger<GetPortalPermissionGroupsForPortalPermissionsDetailsHandler> _logger;
            private readonly IPortalPermissionRepository _repository;
            public GetPortalPermissionGroupsForPortalPermissionsDetailsHandler(ILogger<GetPortalPermissionGroupsForPortalPermissionsDetailsHandler> logger, IPortalPermissionRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<GetPortalPermissionGroupsForPortalPermissionsResponseDto>> Handle(GetPortalPermissionGroupsForPortalPermissionsDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetPortalPermissionGroupsForPortalPermissionsDetailsAsync(request.PortalPermissionsName, request.PortalPermissionGroupsPortalPermissionName);
                return returnValue.ToResponse();
            }
        }
    }
}