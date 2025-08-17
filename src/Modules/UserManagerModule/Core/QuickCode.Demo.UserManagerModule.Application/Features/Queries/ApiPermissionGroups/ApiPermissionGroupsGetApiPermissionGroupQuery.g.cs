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
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features
{
    public class ApiPermissionGroupsGetApiPermissionGroupQuery : IRequest<Response<List<ApiPermissionGroupsGetApiPermissionGroupResponseDto>>>
    {
        public string ApiPermissionGroupsPermissionGroupName { get; set; }
        public string ApiPermissionGroupsApiMethodDefinitionKey { get; set; }

        public ApiPermissionGroupsGetApiPermissionGroupQuery(string apiPermissionGroupsPermissionGroupName, string apiPermissionGroupsApiMethodDefinitionKey)
        {
            this.ApiPermissionGroupsPermissionGroupName = apiPermissionGroupsPermissionGroupName;
            this.ApiPermissionGroupsApiMethodDefinitionKey = apiPermissionGroupsApiMethodDefinitionKey;
        }

        public class ApiPermissionGroupsGetApiPermissionGroupHandler : IRequestHandler<ApiPermissionGroupsGetApiPermissionGroupQuery, Response<List<ApiPermissionGroupsGetApiPermissionGroupResponseDto>>>
        {
            private readonly ILogger<ApiPermissionGroupsGetApiPermissionGroupHandler> _logger;
            private readonly IApiPermissionGroupsRepository _repository;
            public ApiPermissionGroupsGetApiPermissionGroupHandler(ILogger<ApiPermissionGroupsGetApiPermissionGroupHandler> logger, IApiPermissionGroupsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<ApiPermissionGroupsGetApiPermissionGroupResponseDto>>> Handle(ApiPermissionGroupsGetApiPermissionGroupQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ApiPermissionGroupsGetApiPermissionGroupAsync(request.ApiPermissionGroupsPermissionGroupName, request.ApiPermissionGroupsApiMethodDefinitionKey);
                return returnValue.ToResponse();
            }
        }
    }
}