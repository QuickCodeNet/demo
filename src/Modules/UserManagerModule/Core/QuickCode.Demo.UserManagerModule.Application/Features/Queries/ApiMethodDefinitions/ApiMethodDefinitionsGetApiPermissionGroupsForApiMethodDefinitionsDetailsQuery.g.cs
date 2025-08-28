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
    public class ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsDetailsQuery : IRequest<Response<ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsResponseDto>>
    {
        public string ApiMethodDefinitionsKey { get; set; }
        public string ApiPermissionGroupsPermissionGroupName { get; set; }

        public ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsDetailsQuery(string apiMethodDefinitionsKey, string apiPermissionGroupsPermissionGroupName)
        {
            this.ApiMethodDefinitionsKey = apiMethodDefinitionsKey;
            this.ApiPermissionGroupsPermissionGroupName = apiPermissionGroupsPermissionGroupName;
        }

        public class ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsDetailsHandler : IRequestHandler<ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsDetailsQuery, Response<ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsResponseDto>>
        {
            private readonly ILogger<ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsDetailsHandler> _logger;
            private readonly IApiMethodDefinitionsRepository _repository;
            public ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsDetailsHandler(ILogger<ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsDetailsHandler> logger, IApiMethodDefinitionsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsResponseDto>> Handle(ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ApiMethodDefinitionsGetApiPermissionGroupsForApiMethodDefinitionsDetailsAsync(request.ApiMethodDefinitionsKey, request.ApiPermissionGroupsPermissionGroupName);
                return returnValue.ToResponse();
            }
        }
    }
}