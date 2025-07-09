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

namespace QuickCode.Demo.UserManagerModule.Application.Features
{
    public class PortalPermissionGroupsGetPortalPermissionGroupQuery : IRequest<Response<List<PortalPermissionGroupsGetPortalPermissionGroupResponseDto>>>
    {
        public int PortalPermissionGroupsPortalPermissionId { get; set; }
        public int PortalPermissionGroupsPermissionGroupId { get; set; }
        public int PortalPermissionGroupsPortalPermissionTypeId { get; set; }

        public PortalPermissionGroupsGetPortalPermissionGroupQuery(int portalPermissionGroupsPortalPermissionId, int portalPermissionGroupsPermissionGroupId, int portalPermissionGroupsPortalPermissionTypeId)
        {
            this.PortalPermissionGroupsPortalPermissionId = portalPermissionGroupsPortalPermissionId;
            this.PortalPermissionGroupsPermissionGroupId = portalPermissionGroupsPermissionGroupId;
            this.PortalPermissionGroupsPortalPermissionTypeId = portalPermissionGroupsPortalPermissionTypeId;
        }

        public class PortalPermissionGroupsGetPortalPermissionGroupHandler : IRequestHandler<PortalPermissionGroupsGetPortalPermissionGroupQuery, Response<List<PortalPermissionGroupsGetPortalPermissionGroupResponseDto>>>
        {
            private readonly ILogger<PortalPermissionGroupsGetPortalPermissionGroupHandler> _logger;
            private readonly IPortalPermissionGroupsRepository _repository;
            public PortalPermissionGroupsGetPortalPermissionGroupHandler(ILogger<PortalPermissionGroupsGetPortalPermissionGroupHandler> logger, IPortalPermissionGroupsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<PortalPermissionGroupsGetPortalPermissionGroupResponseDto>>> Handle(PortalPermissionGroupsGetPortalPermissionGroupQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PortalPermissionGroupsGetPortalPermissionGroupAsync(request.PortalPermissionGroupsPortalPermissionId, request.PortalPermissionGroupsPermissionGroupId, request.PortalPermissionGroupsPortalPermissionTypeId);
                return returnValue.ToResponse();
            }
        }
    }
}