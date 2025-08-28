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
    public class PermissionGroupsGetPortalPermissionGroupsForPermissionGroupsDetailsQuery : IRequest<Response<PermissionGroupsGetPortalPermissionGroupsForPermissionGroupsResponseDto>>
    {
        public string PermissionGroupsName { get; set; }
        public string PortalPermissionGroupsPortalPermissionName { get; set; }

        public PermissionGroupsGetPortalPermissionGroupsForPermissionGroupsDetailsQuery(string permissionGroupsName, string portalPermissionGroupsPortalPermissionName)
        {
            this.PermissionGroupsName = permissionGroupsName;
            this.PortalPermissionGroupsPortalPermissionName = portalPermissionGroupsPortalPermissionName;
        }

        public class PermissionGroupsGetPortalPermissionGroupsForPermissionGroupsDetailsHandler : IRequestHandler<PermissionGroupsGetPortalPermissionGroupsForPermissionGroupsDetailsQuery, Response<PermissionGroupsGetPortalPermissionGroupsForPermissionGroupsResponseDto>>
        {
            private readonly ILogger<PermissionGroupsGetPortalPermissionGroupsForPermissionGroupsDetailsHandler> _logger;
            private readonly IPermissionGroupsRepository _repository;
            public PermissionGroupsGetPortalPermissionGroupsForPermissionGroupsDetailsHandler(ILogger<PermissionGroupsGetPortalPermissionGroupsForPermissionGroupsDetailsHandler> logger, IPermissionGroupsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PermissionGroupsGetPortalPermissionGroupsForPermissionGroupsResponseDto>> Handle(PermissionGroupsGetPortalPermissionGroupsForPermissionGroupsDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PermissionGroupsGetPortalPermissionGroupsForPermissionGroupsDetailsAsync(request.PermissionGroupsName, request.PortalPermissionGroupsPortalPermissionName);
                return returnValue.ToResponse();
            }
        }
    }
}