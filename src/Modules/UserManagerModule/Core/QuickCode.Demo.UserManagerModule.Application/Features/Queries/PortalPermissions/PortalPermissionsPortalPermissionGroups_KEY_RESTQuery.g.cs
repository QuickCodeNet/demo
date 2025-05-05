using System;
using System.Linq;
using MediatR;
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
    public class PortalPermissionsPortalPermissionsPortalPermissionGroups_KEY_RESTQuery : IRequest<Response<PortalPermissionsPortalPermissionGroups_KEY_RESTResponseDto>>
    {
        public int PortalPermissionsId { get; set; }
        public int PortalPermissionGroupsId { get; set; }

        public PortalPermissionsPortalPermissionsPortalPermissionGroups_KEY_RESTQuery(int portalPermissionsId, int portalPermissionGroupsId)
        {
            this.PortalPermissionsId = portalPermissionsId;
            this.PortalPermissionGroupsId = portalPermissionGroupsId;
        }

        public class PortalPermissionsPortalPermissionsPortalPermissionGroups_KEY_RESTHandler : IRequestHandler<PortalPermissionsPortalPermissionsPortalPermissionGroups_KEY_RESTQuery, Response<PortalPermissionsPortalPermissionGroups_KEY_RESTResponseDto>>
        {
            private readonly ILogger<PortalPermissionsPortalPermissionsPortalPermissionGroups_KEY_RESTHandler> _logger;
            private readonly IPortalPermissionsRepository _repository;
            public PortalPermissionsPortalPermissionsPortalPermissionGroups_KEY_RESTHandler(ILogger<PortalPermissionsPortalPermissionsPortalPermissionGroups_KEY_RESTHandler> logger, IPortalPermissionsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PortalPermissionsPortalPermissionGroups_KEY_RESTResponseDto>> Handle(PortalPermissionsPortalPermissionsPortalPermissionGroups_KEY_RESTQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PortalPermissionsPortalPermissionGroups_KEY_RESTAsync(request.PortalPermissionsId, request.PortalPermissionGroupsId);
                return returnValue.ToResponse();
            }
        }
    }
}