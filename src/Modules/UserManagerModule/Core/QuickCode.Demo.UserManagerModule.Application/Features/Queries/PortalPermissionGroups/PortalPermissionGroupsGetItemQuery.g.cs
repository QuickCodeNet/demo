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
    public class PortalPermissionGroupsGetItemQuery : IRequest<Response<PortalPermissionGroupsDto>>
    {
        public string PortalPermissionName { get; set; }
        public string PermissionGroupName { get; set; }
        public int PortalPermissionTypeId { get; set; }

        public PortalPermissionGroupsGetItemQuery(string portalPermissionName, string permissionGroupName, int portalPermissionTypeId)
        {
            this.PortalPermissionName = portalPermissionName;
            this.PermissionGroupName = permissionGroupName;
            this.PortalPermissionTypeId = portalPermissionTypeId;
        }

        public class PortalPermissionGroupsGetItemHandler : IRequestHandler<PortalPermissionGroupsGetItemQuery, Response<PortalPermissionGroupsDto>>
        {
            private readonly ILogger<PortalPermissionGroupsGetItemHandler> _logger;
            private readonly IPortalPermissionGroupsRepository _repository;
            public PortalPermissionGroupsGetItemHandler(ILogger<PortalPermissionGroupsGetItemHandler> logger, IPortalPermissionGroupsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PortalPermissionGroupsDto>> Handle(PortalPermissionGroupsGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.PortalPermissionName, request.PermissionGroupName, request.PortalPermissionTypeId);
                return returnValue.ToResponse();
            }
        }
    }
}