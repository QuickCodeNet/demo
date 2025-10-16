using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.UserManagerModule.Domain.Entities;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.UserManagerModule.Application.Dtos.ApiPermissionGroup;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.ApiPermissionGroup
{
    public class GetItemApiPermissionGroupQuery : IRequest<Response<ApiPermissionGroupDto>>
    {
        public string PermissionGroupName { get; set; }
        public string ApiMethodDefinitionKey { get; set; }

        public GetItemApiPermissionGroupQuery(string permissionGroupName, string apiMethodDefinitionKey)
        {
            this.PermissionGroupName = permissionGroupName;
            this.ApiMethodDefinitionKey = apiMethodDefinitionKey;
        }

        public class GetItemApiPermissionGroupHandler : IRequestHandler<GetItemApiPermissionGroupQuery, Response<ApiPermissionGroupDto>>
        {
            private readonly ILogger<GetItemApiPermissionGroupHandler> _logger;
            private readonly IApiPermissionGroupRepository _repository;
            public GetItemApiPermissionGroupHandler(ILogger<GetItemApiPermissionGroupHandler> logger, IApiPermissionGroupRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<ApiPermissionGroupDto>> Handle(GetItemApiPermissionGroupQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.PermissionGroupName, request.ApiMethodDefinitionKey);
                return returnValue.ToResponse();
            }
        }
    }
}