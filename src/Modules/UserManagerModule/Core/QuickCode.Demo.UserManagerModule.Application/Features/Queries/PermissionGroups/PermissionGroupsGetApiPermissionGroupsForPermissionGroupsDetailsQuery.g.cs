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
    public class PermissionGroupsGetApiPermissionGroupsForPermissionGroupsDetailsQuery : IRequest<Response<PermissionGroupsGetApiPermissionGroupsForPermissionGroupsResponseDto>>
    {
        public string PermissionGroupsName { get; set; }
        public string ApiPermissionGroupsPermissionGroupName { get; set; }

        public PermissionGroupsGetApiPermissionGroupsForPermissionGroupsDetailsQuery(string permissionGroupsName, string apiPermissionGroupsPermissionGroupName)
        {
            this.PermissionGroupsName = permissionGroupsName;
            this.ApiPermissionGroupsPermissionGroupName = apiPermissionGroupsPermissionGroupName;
        }

        public class PermissionGroupsGetApiPermissionGroupsForPermissionGroupsDetailsHandler : IRequestHandler<PermissionGroupsGetApiPermissionGroupsForPermissionGroupsDetailsQuery, Response<PermissionGroupsGetApiPermissionGroupsForPermissionGroupsResponseDto>>
        {
            private readonly ILogger<PermissionGroupsGetApiPermissionGroupsForPermissionGroupsDetailsHandler> _logger;
            private readonly IPermissionGroupsRepository _repository;
            public PermissionGroupsGetApiPermissionGroupsForPermissionGroupsDetailsHandler(ILogger<PermissionGroupsGetApiPermissionGroupsForPermissionGroupsDetailsHandler> logger, IPermissionGroupsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PermissionGroupsGetApiPermissionGroupsForPermissionGroupsResponseDto>> Handle(PermissionGroupsGetApiPermissionGroupsForPermissionGroupsDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PermissionGroupsGetApiPermissionGroupsForPermissionGroupsDetailsAsync(request.PermissionGroupsName, request.ApiPermissionGroupsPermissionGroupName);
                return returnValue.ToResponse();
            }
        }
    }
}