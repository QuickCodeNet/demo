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
    public class GetPortalPermissionGroupsForPermissionGroupsQuery : IRequest<Response<List<GetPortalPermissionGroupsForPermissionGroupsResponseDto>>>
    {
        public string PermissionGroupsName { get; set; }

        public GetPortalPermissionGroupsForPermissionGroupsQuery(string permissionGroupsName)
        {
            this.PermissionGroupsName = permissionGroupsName;
        }

        public class GetPortalPermissionGroupsForPermissionGroupsHandler : IRequestHandler<GetPortalPermissionGroupsForPermissionGroupsQuery, Response<List<GetPortalPermissionGroupsForPermissionGroupsResponseDto>>>
        {
            private readonly ILogger<GetPortalPermissionGroupsForPermissionGroupsHandler> _logger;
            private readonly IPermissionGroupRepository _repository;
            public GetPortalPermissionGroupsForPermissionGroupsHandler(ILogger<GetPortalPermissionGroupsForPermissionGroupsHandler> logger, IPermissionGroupRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetPortalPermissionGroupsForPermissionGroupsResponseDto>>> Handle(GetPortalPermissionGroupsForPermissionGroupsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetPortalPermissionGroupsForPermissionGroupsAsync(request.PermissionGroupsName);
                return returnValue.ToResponse();
            }
        }
    }
}