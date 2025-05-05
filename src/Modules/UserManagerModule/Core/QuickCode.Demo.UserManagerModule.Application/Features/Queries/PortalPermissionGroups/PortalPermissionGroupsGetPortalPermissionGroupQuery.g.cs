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
    public class PortalPermissionGroupsPortalPermissionGroupsGetPortalPermissionGroupQuery : IRequest<Response<List<PortalPermissionGroupsGetPortalPermissionGroupResponseDto>>>
    {
        public int PortalPermissionGroupsPortalPermissionId { get; set; }
        public int PortalPermissionGroupsPermissionGroupId { get; set; }
        public int PortalPermissionGroupsPortalPermissionTypeId { get; set; }

        public PortalPermissionGroupsPortalPermissionGroupsGetPortalPermissionGroupQuery(int portalPermissionGroupsPortalPermissionId, int portalPermissionGroupsPermissionGroupId, int portalPermissionGroupsPortalPermissionTypeId)
        {
            this.PortalPermissionGroupsPortalPermissionId = portalPermissionGroupsPortalPermissionId;
            this.PortalPermissionGroupsPermissionGroupId = portalPermissionGroupsPermissionGroupId;
            this.PortalPermissionGroupsPortalPermissionTypeId = portalPermissionGroupsPortalPermissionTypeId;
        }

        public class PortalPermissionGroupsPortalPermissionGroupsGetPortalPermissionGroupHandler : IRequestHandler<PortalPermissionGroupsPortalPermissionGroupsGetPortalPermissionGroupQuery, Response<List<PortalPermissionGroupsGetPortalPermissionGroupResponseDto>>>
        {
            private readonly ILogger<PortalPermissionGroupsPortalPermissionGroupsGetPortalPermissionGroupHandler> _logger;
            private readonly IPortalPermissionGroupsRepository _repository;
            public PortalPermissionGroupsPortalPermissionGroupsGetPortalPermissionGroupHandler(ILogger<PortalPermissionGroupsPortalPermissionGroupsGetPortalPermissionGroupHandler> logger, IPortalPermissionGroupsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<PortalPermissionGroupsGetPortalPermissionGroupResponseDto>>> Handle(PortalPermissionGroupsPortalPermissionGroupsGetPortalPermissionGroupQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PortalPermissionGroupsGetPortalPermissionGroupAsync(request.PortalPermissionGroupsPortalPermissionId, request.PortalPermissionGroupsPermissionGroupId, request.PortalPermissionGroupsPortalPermissionTypeId);
                return returnValue.ToResponse();
            }
        }
    }
}