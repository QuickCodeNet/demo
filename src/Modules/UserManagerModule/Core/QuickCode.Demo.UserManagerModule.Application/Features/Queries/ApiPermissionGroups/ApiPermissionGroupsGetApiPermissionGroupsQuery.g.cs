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
    public class ApiPermissionGroupsGetApiPermissionGroupsQuery : IRequest<Response<List<ApiPermissionGroupsGetApiPermissionGroupsResponseDto>>>
    {
        public string ApiPermissionGroupsPermissionGroupName { get; set; }

        public ApiPermissionGroupsGetApiPermissionGroupsQuery(string apiPermissionGroupsPermissionGroupName)
        {
            this.ApiPermissionGroupsPermissionGroupName = apiPermissionGroupsPermissionGroupName;
        }

        public class ApiPermissionGroupsGetApiPermissionGroupsHandler : IRequestHandler<ApiPermissionGroupsGetApiPermissionGroupsQuery, Response<List<ApiPermissionGroupsGetApiPermissionGroupsResponseDto>>>
        {
            private readonly ILogger<ApiPermissionGroupsGetApiPermissionGroupsHandler> _logger;
            private readonly IApiPermissionGroupsRepository _repository;
            public ApiPermissionGroupsGetApiPermissionGroupsHandler(ILogger<ApiPermissionGroupsGetApiPermissionGroupsHandler> logger, IApiPermissionGroupsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<ApiPermissionGroupsGetApiPermissionGroupsResponseDto>>> Handle(ApiPermissionGroupsGetApiPermissionGroupsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ApiPermissionGroupsGetApiPermissionGroupsAsync(request.ApiPermissionGroupsPermissionGroupName);
                return returnValue.ToResponse();
            }
        }
    }
}