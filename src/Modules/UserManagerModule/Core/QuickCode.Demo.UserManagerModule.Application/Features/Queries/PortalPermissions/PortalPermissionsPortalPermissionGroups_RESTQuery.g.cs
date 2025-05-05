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
    public class PortalPermissionsPortalPermissionsPortalPermissionGroups_RESTQuery : IRequest<Response<List<PortalPermissionsPortalPermissionGroups_RESTResponseDto>>>
    {
        public int PortalPermissionsId { get; set; }

        public PortalPermissionsPortalPermissionsPortalPermissionGroups_RESTQuery(int portalPermissionsId)
        {
            this.PortalPermissionsId = portalPermissionsId;
        }

        public class PortalPermissionsPortalPermissionsPortalPermissionGroups_RESTHandler : IRequestHandler<PortalPermissionsPortalPermissionsPortalPermissionGroups_RESTQuery, Response<List<PortalPermissionsPortalPermissionGroups_RESTResponseDto>>>
        {
            private readonly ILogger<PortalPermissionsPortalPermissionsPortalPermissionGroups_RESTHandler> _logger;
            private readonly IPortalPermissionsRepository _repository;
            public PortalPermissionsPortalPermissionsPortalPermissionGroups_RESTHandler(ILogger<PortalPermissionsPortalPermissionsPortalPermissionGroups_RESTHandler> logger, IPortalPermissionsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<PortalPermissionsPortalPermissionGroups_RESTResponseDto>>> Handle(PortalPermissionsPortalPermissionsPortalPermissionGroups_RESTQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PortalPermissionsPortalPermissionGroups_RESTAsync(request.PortalPermissionsId);
                return returnValue.ToResponse();
            }
        }
    }
}