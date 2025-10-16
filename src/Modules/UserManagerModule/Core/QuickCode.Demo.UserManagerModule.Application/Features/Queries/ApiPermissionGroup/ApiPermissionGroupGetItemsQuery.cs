using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.UserManagerModule.Application.Dtos;
using QuickCode.Demo.UserManagerModule.Application.Models;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;

namespace QuickCode.Demo.UserManagerModule.Api.Application.Features.Queries.ApiPermissionGroup
{
    public class ApiPermissionGroupGetItemsQuery : IRequest<Response<ApiModulePermissions>>
    {
        public string permissionGroupName { get; set; }

        public ApiPermissionGroupGetItemsQuery(string permissionGroupName)
        {
            this.permissionGroupName = permissionGroupName;
        }

        public class AuthorizationsApiGroupsGetItemsHandler : IRequestHandler<ApiPermissionGroupGetItemsQuery, Response<ApiModulePermissions>>
        {
            private readonly ILogger<AuthorizationsApiGroupsGetItemsHandler> _logger;
            private readonly IApiPermissionGroupRepository _apiPermissionGroupRepository;
            private readonly IApiMethodDefinitionRepository _apiMethodDefinitionRepository;
            
            public AuthorizationsApiGroupsGetItemsHandler(ILogger<AuthorizationsApiGroupsGetItemsHandler> logger, 
                IApiPermissionGroupRepository apiPermissionGroupRepository,
                IApiMethodDefinitionRepository apiPermissionsRepository)
            {
                _logger = logger;
                _apiPermissionGroupRepository = apiPermissionGroupRepository;
                _apiMethodDefinitionRepository = apiPermissionsRepository;
            }

            public async Task<Response<ApiModulePermissions>> Handle(ApiPermissionGroupGetItemsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = new Response<ApiModulePermissions>
                {
                    Value = new ApiModulePermissions
                    {
                        PermissionGroupName = request.permissionGroupName,
                        ApiModulePermissionList = []
                    }
                };
                
                var authorizationsApis = (await _apiMethodDefinitionRepository.ListAsync()).Value.OrderBy(i => i.UrlPath)
                    .ThenBy(i => i.ControllerName);

                var authorizationApiGroupData =
                    (await _apiPermissionGroupRepository.GetApiPermissionGroupNamesAsync(
                        request.permissionGroupName)).Value;
                foreach (var authorizationApi in authorizationsApis.OrderBy(i => i.ControllerName)
                             .ThenBy(i => i.HttpMethod))
                {

                    var isExists = authorizationApiGroupData.Exists(i =>
                        i.PermissionGroupName.Equals(request.permissionGroupName) && i.ApiMethodDefinitionKey.Equals(authorizationApi.Key));

                    var item = new ApiMethodDefinitionItem
                    {
                        Key = authorizationApi.Key,
                        ControllerName = authorizationApi.ControllerName,
                        HttpMethod = authorizationApi.HttpMethod,
                        UrlPath = authorizationApi.UrlPath,
                        Value = isExists
                    };

                    var controllerItems = new Dictionary<string, List<ApiMethodDefinitionItem>>();
                    var itemList = new List<ApiMethodDefinitionItem>();
                    var moduleName = item.UrlPath.Split('/')[2].KebabCaseToPascal();

                    if (item.ControllerName.Equals("AuthenticationsController"))
                    {
                        continue;
                    }

                    var controllerName = item.ControllerName[0..^"Controller".Length].PascalToKebabCase().KebabCaseToPascal();
                    returnValue.Value.ApiModulePermissionList.TryAdd(moduleName, controllerItems);
                    controllerItems = returnValue.Value.ApiModulePermissionList[moduleName];
                    controllerItems.TryAdd(controllerName, itemList);
                    returnValue.Value.ApiModulePermissionList[moduleName][controllerName].Add(item);
                }

                return returnValue;
            }
        }
    }
}
