using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.UserManagerModule.Application.Models;
using QuickCode.Demo.UserManagerModule.Domain.Entities;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.UserManagerModule.Application.Dtos;

namespace QuickCode.Demo.UserManagerModule.Application.Features.Queries.PortalPermissionGroups
{
    public class PortalPermissionGroupsGetItemsQuery : IRequest<Response<PortalPermissionGroupList>>
    {
        private string permissionGroupName { get; set; }

        public PortalPermissionGroupsGetItemsQuery(string permissionGroupName)
        {
            this.permissionGroupName = permissionGroupName;
        }

        public class PortalPermissionGroupsGetItemsHandler : IRequestHandler<PortalPermissionGroupsGetItemsQuery, Response<PortalPermissionGroupList>>
        {
            private readonly ILogger<PortalPermissionGroupsGetItemsHandler> _logger;
            private readonly IPortalPermissionGroupsRepository _portalPermissionGroupsRepository;
            private readonly IPortalPermissionTypesRepository _portalPermissionTypesRepository;
            private readonly IPortalPermissionsRepository _portalPermissionsRepository;
            
            public PortalPermissionGroupsGetItemsHandler(ILogger<PortalPermissionGroupsGetItemsHandler> logger, 
                IPortalPermissionGroupsRepository portalPermissionGroupsRepository,
                IPortalPermissionTypesRepository portalPermissionTypesRepository,
                IPortalPermissionsRepository portalPermissionsRepository)
            {
                _logger = logger;
                _portalPermissionGroupsRepository = portalPermissionGroupsRepository;
                _portalPermissionTypesRepository = portalPermissionTypesRepository;
                _portalPermissionsRepository= portalPermissionsRepository;
            }

            public async Task<Response<PortalPermissionGroupList>> Handle(PortalPermissionGroupsGetItemsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = new Response<PortalPermissionGroupList>
                {
                    Value = new PortalPermissionGroupList
                    {
                        PermissionGroupName = request.permissionGroupName,
                        PortalPermissions = []
                    }
                };

                var permissionTypes = (await _portalPermissionTypesRepository.ListAsync()).Value;
                var permissions = (await _portalPermissionsRepository.ListAsync()).Value;
                var permissionGroupData = (await _portalPermissionGroupsRepository.PortalPermissionGroupsGetPortalPermissionGroupsAsync(request.permissionGroupName)).Value;
                foreach (var portalPermission in permissions.OrderBy(i=>i.Name))
                {
                    var item = new PortalPermissionItem()
                    {
                        PortalPermissionName = portalPermission.Name,
                        PortalPermissionTypes = []
                    };

                    foreach (var type in permissionTypes)
                    {
                        var typeItem = new PortalPermissionTypeItem()
                        {
                            PortalPermissionTypeId = type.Id
                        };
                        
                        var result = permissionGroupData!.Where(i =>
                            i.PortalPermissionName == portalPermission.Name && i.PortalPermissionTypeId == type.Id);
                        typeItem.Value = result.Any();
                        item.PortalPermissionTypes.Add(typeItem);
                    }

                    returnValue.Value.PortalPermissions.Add(item);
                }

                return returnValue;
            }
        }
    }
}
