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
    public class PermissionGroupsGetPortalPermissionGroupsForPermissionGroupsQuery : IRequest<Response<List<PermissionGroupsGetPortalPermissionGroupsForPermissionGroupsResponseDto>>>
    {
        public string PermissionGroupsName { get; set; }

        public PermissionGroupsGetPortalPermissionGroupsForPermissionGroupsQuery(string permissionGroupsName)
        {
            this.PermissionGroupsName = permissionGroupsName;
        }

        public class PermissionGroupsGetPortalPermissionGroupsForPermissionGroupsHandler : IRequestHandler<PermissionGroupsGetPortalPermissionGroupsForPermissionGroupsQuery, Response<List<PermissionGroupsGetPortalPermissionGroupsForPermissionGroupsResponseDto>>>
        {
            private readonly ILogger<PermissionGroupsGetPortalPermissionGroupsForPermissionGroupsHandler> _logger;
            private readonly IPermissionGroupsRepository _repository;
            public PermissionGroupsGetPortalPermissionGroupsForPermissionGroupsHandler(ILogger<PermissionGroupsGetPortalPermissionGroupsForPermissionGroupsHandler> logger, IPermissionGroupsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<PermissionGroupsGetPortalPermissionGroupsForPermissionGroupsResponseDto>>> Handle(PermissionGroupsGetPortalPermissionGroupsForPermissionGroupsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PermissionGroupsGetPortalPermissionGroupsForPermissionGroupsAsync(request.PermissionGroupsName);
                return returnValue.ToResponse();
            }
        }
    }
}