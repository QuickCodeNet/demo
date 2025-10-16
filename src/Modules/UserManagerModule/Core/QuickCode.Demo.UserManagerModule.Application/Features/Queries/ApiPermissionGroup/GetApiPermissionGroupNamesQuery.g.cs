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
using QuickCode.Demo.UserManagerModule.Application.Dtos.ApiPermissionGroup;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.ApiPermissionGroup
{
    public class GetApiPermissionGroupNamesQuery : IRequest<Response<List<GetApiPermissionGroupNamesResponseDto>>>
    {
        public string ApiPermissionGroupsPermissionGroupName { get; set; }

        public GetApiPermissionGroupNamesQuery(string apiPermissionGroupsPermissionGroupName)
        {
            this.ApiPermissionGroupsPermissionGroupName = apiPermissionGroupsPermissionGroupName;
        }

        public class GetApiPermissionGroupNamesHandler : IRequestHandler<GetApiPermissionGroupNamesQuery, Response<List<GetApiPermissionGroupNamesResponseDto>>>
        {
            private readonly ILogger<GetApiPermissionGroupNamesHandler> _logger;
            private readonly IApiPermissionGroupRepository _repository;
            public GetApiPermissionGroupNamesHandler(ILogger<GetApiPermissionGroupNamesHandler> logger, IApiPermissionGroupRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetApiPermissionGroupNamesResponseDto>>> Handle(GetApiPermissionGroupNamesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetApiPermissionGroupNamesAsync(request.ApiPermissionGroupsPermissionGroupName);
                return returnValue.ToResponse();
            }
        }
    }
}