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

namespace QuickCode.Demo.UserManagerModule.Application.Features.Queries.PortalPermissionGroup
{
    public class PortalPermissionGroupGetItemsQuery : IRequest<Response<PortalPermissionGroupList>>
    {
        private string permissionGroupName { get; set; }

        public PortalPermissionGroupGetItemsQuery(string permissionGroupName)
        {
            this.permissionGroupName = permissionGroupName;
        }

        public class PortalPermissionGroupGetItemsHandler : IRequestHandler<PortalPermissionGroupGetItemsQuery, Response<PortalPermissionGroupList>>
        {
            private readonly ILogger<PortalPermissionGroupGetItemsHandler> _logger;
            private readonly IPortalPermissionGroupRepository _portalPermissionGroupRepository;
            private readonly IPortalPermissionTypeRepository _portalPermissionTypeRepository;
            private readonly IPortalPermissionRepository _portalPermissionRepository;
            
            public PortalPermissionGroupGetItemsHandler(ILogger<PortalPermissionGroupGetItemsHandler> logger, 
                IPortalPermissionGroupRepository portalPermissionGroupRepository,
                IPortalPermissionTypeRepository portalPermissionTypeRepository,
                IPortalPermissionRepository portalPermissionRepository)
            {
                _logger = logger;
                _portalPermissionGroupRepository = portalPermissionGroupRepository;
                _portalPermissionTypeRepository = portalPermissionTypeRepository;
                _portalPermissionRepository= portalPermissionRepository;
            }

            public async Task<Response<PortalPermissionGroupList>> Handle(PortalPermissionGroupGetItemsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = new Response<PortalPermissionGroupList>
                {
                    Value = new PortalPermissionGroupList
                    {
                        PermissionGroupName = request.permissionGroupName,
                        PortalPermissions = []
                    }
                };

                var permissionTypes = (await _portalPermissionTypeRepository.ListAsync()).Value;
                var permissions = (await _portalPermissionRepository.ListAsync()).Value;
                var permissionGroupData = (await _portalPermissionGroupRepository.GetPortalPermissionGroupsAsync(request.permissionGroupName)).Value;
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
