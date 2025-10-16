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
using QuickCode.Demo.UserManagerModule.Application.Dtos.PermissionGroup;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.PermissionGroup
{
    public class GetPortalPermissionGroupsForPermissionGroupsDetailsQuery : IRequest<Response<GetPortalPermissionGroupsForPermissionGroupsResponseDto>>
    {
        public string PermissionGroupsName { get; set; }
        public string PortalPermissionGroupsPortalPermissionName { get; set; }

        public GetPortalPermissionGroupsForPermissionGroupsDetailsQuery(string permissionGroupsName, string portalPermissionGroupsPortalPermissionName)
        {
            this.PermissionGroupsName = permissionGroupsName;
            this.PortalPermissionGroupsPortalPermissionName = portalPermissionGroupsPortalPermissionName;
        }

        public class GetPortalPermissionGroupsForPermissionGroupsDetailsHandler : IRequestHandler<GetPortalPermissionGroupsForPermissionGroupsDetailsQuery, Response<GetPortalPermissionGroupsForPermissionGroupsResponseDto>>
        {
            private readonly ILogger<GetPortalPermissionGroupsForPermissionGroupsDetailsHandler> _logger;
            private readonly IPermissionGroupRepository _repository;
            public GetPortalPermissionGroupsForPermissionGroupsDetailsHandler(ILogger<GetPortalPermissionGroupsForPermissionGroupsDetailsHandler> logger, IPermissionGroupRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<GetPortalPermissionGroupsForPermissionGroupsResponseDto>> Handle(GetPortalPermissionGroupsForPermissionGroupsDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetPortalPermissionGroupsForPermissionGroupsDetailsAsync(request.PermissionGroupsName, request.PortalPermissionGroupsPortalPermissionName);
                return returnValue.ToResponse();
            }
        }
    }
}