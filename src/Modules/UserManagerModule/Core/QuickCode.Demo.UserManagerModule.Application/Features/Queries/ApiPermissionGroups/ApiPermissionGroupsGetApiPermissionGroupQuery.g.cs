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
    public class ApiPermissionGroupsApiPermissionGroupsGetApiPermissionGroupQuery : IRequest<Response<List<ApiPermissionGroupsGetApiPermissionGroupResponseDto>>>
    {
        public int ApiPermissionGroupsPermissionGroupId { get; set; }
        public int ApiPermissionGroupsApiMethodDefinitionId { get; set; }

        public ApiPermissionGroupsApiPermissionGroupsGetApiPermissionGroupQuery(int apiPermissionGroupsPermissionGroupId, int apiPermissionGroupsApiMethodDefinitionId)
        {
            this.ApiPermissionGroupsPermissionGroupId = apiPermissionGroupsPermissionGroupId;
            this.ApiPermissionGroupsApiMethodDefinitionId = apiPermissionGroupsApiMethodDefinitionId;
        }

        public class ApiPermissionGroupsApiPermissionGroupsGetApiPermissionGroupHandler : IRequestHandler<ApiPermissionGroupsApiPermissionGroupsGetApiPermissionGroupQuery, Response<List<ApiPermissionGroupsGetApiPermissionGroupResponseDto>>>
        {
            private readonly ILogger<ApiPermissionGroupsApiPermissionGroupsGetApiPermissionGroupHandler> _logger;
            private readonly IApiPermissionGroupsRepository _repository;
            public ApiPermissionGroupsApiPermissionGroupsGetApiPermissionGroupHandler(ILogger<ApiPermissionGroupsApiPermissionGroupsGetApiPermissionGroupHandler> logger, IApiPermissionGroupsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<ApiPermissionGroupsGetApiPermissionGroupResponseDto>>> Handle(ApiPermissionGroupsApiPermissionGroupsGetApiPermissionGroupQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ApiPermissionGroupsGetApiPermissionGroupAsync(request.ApiPermissionGroupsPermissionGroupId, request.ApiPermissionGroupsApiMethodDefinitionId);
                return returnValue.ToResponse();
            }
        }
    }
}