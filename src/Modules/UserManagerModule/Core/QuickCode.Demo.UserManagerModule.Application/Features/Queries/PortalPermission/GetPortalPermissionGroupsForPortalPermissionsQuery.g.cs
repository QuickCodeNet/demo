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
    public class GetPortalPermissionGroupsForPortalPermissionsQuery : IRequest<Response<List<GetPortalPermissionGroupsForPortalPermissionsResponseDto>>>
    {
        public string PortalPermissionsName { get; set; }

        public GetPortalPermissionGroupsForPortalPermissionsQuery(string portalPermissionsName)
        {
            this.PortalPermissionsName = portalPermissionsName;
        }

        public class GetPortalPermissionGroupsForPortalPermissionsHandler : IRequestHandler<GetPortalPermissionGroupsForPortalPermissionsQuery, Response<List<GetPortalPermissionGroupsForPortalPermissionsResponseDto>>>
        {
            private readonly ILogger<GetPortalPermissionGroupsForPortalPermissionsHandler> _logger;
            private readonly IPortalPermissionRepository _repository;
            public GetPortalPermissionGroupsForPortalPermissionsHandler(ILogger<GetPortalPermissionGroupsForPortalPermissionsHandler> logger, IPortalPermissionRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetPortalPermissionGroupsForPortalPermissionsResponseDto>>> Handle(GetPortalPermissionGroupsForPortalPermissionsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetPortalPermissionGroupsForPortalPermissionsAsync(request.PortalPermissionsName);
                return returnValue.ToResponse();
            }
        }
    }
}