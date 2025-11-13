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
using QuickCode.Demo.UserManagerModule.Application.Dtos.PortalPermissionGroup;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.PortalPermissionGroup
{
    public class GetPortalPermissionGroupQuery : IRequest<Response<List<GetPortalPermissionGroupResponseDto>>>
    {
        public string PortalPermissionGroupsPortalPermissionName { get; set; }
        public string PortalPermissionGroupsPermissionGroupName { get; set; }
        public int PortalPermissionGroupsPortalPermissionTypeId { get; set; }

        public GetPortalPermissionGroupQuery(string portalPermissionGroupsPortalPermissionName, string portalPermissionGroupsPermissionGroupName, int portalPermissionGroupsPortalPermissionTypeId)
        {
            this.PortalPermissionGroupsPortalPermissionName = portalPermissionGroupsPortalPermissionName;
            this.PortalPermissionGroupsPermissionGroupName = portalPermissionGroupsPermissionGroupName;
            this.PortalPermissionGroupsPortalPermissionTypeId = portalPermissionGroupsPortalPermissionTypeId;
        }

        public class GetPortalPermissionGroupHandler : IRequestHandler<GetPortalPermissionGroupQuery, Response<List<GetPortalPermissionGroupResponseDto>>>
        {
            private readonly ILogger<GetPortalPermissionGroupHandler> _logger;
            private readonly IPortalPermissionGroupRepository _repository;
            public GetPortalPermissionGroupHandler(ILogger<GetPortalPermissionGroupHandler> logger, IPortalPermissionGroupRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetPortalPermissionGroupResponseDto>>> Handle(GetPortalPermissionGroupQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetPortalPermissionGroupAsync(request.PortalPermissionGroupsPortalPermissionName, request.PortalPermissionGroupsPermissionGroupName, request.PortalPermissionGroupsPortalPermissionTypeId);
                return returnValue.ToResponse();
            }
        }
    }
}