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
    public class GetApiPermissionGroupsForPermissionGroupsDetailsQuery : IRequest<Response<GetApiPermissionGroupsForPermissionGroupsResponseDto>>
    {
        public string PermissionGroupsName { get; set; }
        public string ApiPermissionGroupsPermissionGroupName { get; set; }

        public GetApiPermissionGroupsForPermissionGroupsDetailsQuery(string permissionGroupsName, string apiPermissionGroupsPermissionGroupName)
        {
            this.PermissionGroupsName = permissionGroupsName;
            this.ApiPermissionGroupsPermissionGroupName = apiPermissionGroupsPermissionGroupName;
        }

        public class GetApiPermissionGroupsForPermissionGroupsDetailsHandler : IRequestHandler<GetApiPermissionGroupsForPermissionGroupsDetailsQuery, Response<GetApiPermissionGroupsForPermissionGroupsResponseDto>>
        {
            private readonly ILogger<GetApiPermissionGroupsForPermissionGroupsDetailsHandler> _logger;
            private readonly IPermissionGroupRepository _repository;
            public GetApiPermissionGroupsForPermissionGroupsDetailsHandler(ILogger<GetApiPermissionGroupsForPermissionGroupsDetailsHandler> logger, IPermissionGroupRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<GetApiPermissionGroupsForPermissionGroupsResponseDto>> Handle(GetApiPermissionGroupsForPermissionGroupsDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetApiPermissionGroupsForPermissionGroupsDetailsAsync(request.PermissionGroupsName, request.ApiPermissionGroupsPermissionGroupName);
                return returnValue.ToResponse();
            }
        }
    }
}