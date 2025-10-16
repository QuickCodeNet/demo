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
    public class GetItemPortalPermissionGroupQuery : IRequest<Response<PortalPermissionGroupDto>>
    {
        public string PortalPermissionName { get; set; }
        public string PermissionGroupName { get; set; }
        public int PortalPermissionTypeId { get; set; }

        public GetItemPortalPermissionGroupQuery(string portalPermissionName, string permissionGroupName, int portalPermissionTypeId)
        {
            this.PortalPermissionName = portalPermissionName;
            this.PermissionGroupName = permissionGroupName;
            this.PortalPermissionTypeId = portalPermissionTypeId;
        }

        public class GetItemPortalPermissionGroupHandler : IRequestHandler<GetItemPortalPermissionGroupQuery, Response<PortalPermissionGroupDto>>
        {
            private readonly ILogger<GetItemPortalPermissionGroupHandler> _logger;
            private readonly IPortalPermissionGroupRepository _repository;
            public GetItemPortalPermissionGroupHandler(ILogger<GetItemPortalPermissionGroupHandler> logger, IPortalPermissionGroupRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PortalPermissionGroupDto>> Handle(GetItemPortalPermissionGroupQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.PortalPermissionName, request.PermissionGroupName, request.PortalPermissionTypeId);
                return returnValue.ToResponse();
            }
        }
    }
}