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
using QuickCode.Demo.UserManagerModule.Application.Dtos.PermissionGroup;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.PermissionGroup
{
    public class GetApiPermissionGroupsForPermissionGroupsQuery : IRequest<Response<List<GetApiPermissionGroupsForPermissionGroupsResponseDto>>>
    {
        public string PermissionGroupsName { get; set; }

        public GetApiPermissionGroupsForPermissionGroupsQuery(string permissionGroupsName)
        {
            this.PermissionGroupsName = permissionGroupsName;
        }

        public class GetApiPermissionGroupsForPermissionGroupsHandler : IRequestHandler<GetApiPermissionGroupsForPermissionGroupsQuery, Response<List<GetApiPermissionGroupsForPermissionGroupsResponseDto>>>
        {
            private readonly ILogger<GetApiPermissionGroupsForPermissionGroupsHandler> _logger;
            private readonly IPermissionGroupRepository _repository;
            public GetApiPermissionGroupsForPermissionGroupsHandler(ILogger<GetApiPermissionGroupsForPermissionGroupsHandler> logger, IPermissionGroupRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetApiPermissionGroupsForPermissionGroupsResponseDto>>> Handle(GetApiPermissionGroupsForPermissionGroupsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetApiPermissionGroupsForPermissionGroupsAsync(request.PermissionGroupsName);
                return returnValue.ToResponse();
            }
        }
    }
}