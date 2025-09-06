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
    public class ApiPermissionGroupsGetItemQuery : IRequest<Response<ApiPermissionGroupsDto>>
    {
        public string PermissionGroupName { get; set; }
        public string ApiMethodDefinitionKey { get; set; }

        public ApiPermissionGroupsGetItemQuery(string permissionGroupName, string apiMethodDefinitionKey)
        {
            this.PermissionGroupName = permissionGroupName;
            this.ApiMethodDefinitionKey = apiMethodDefinitionKey;
        }

        public class ApiPermissionGroupsGetItemHandler : IRequestHandler<ApiPermissionGroupsGetItemQuery, Response<ApiPermissionGroupsDto>>
        {
            private readonly ILogger<ApiPermissionGroupsGetItemHandler> _logger;
            private readonly IApiPermissionGroupsRepository _repository;
            public ApiPermissionGroupsGetItemHandler(ILogger<ApiPermissionGroupsGetItemHandler> logger, IApiPermissionGroupsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<ApiPermissionGroupsDto>> Handle(ApiPermissionGroupsGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.PermissionGroupName, request.ApiMethodDefinitionKey);
                return returnValue.ToResponse();
            }
        }
    }
}