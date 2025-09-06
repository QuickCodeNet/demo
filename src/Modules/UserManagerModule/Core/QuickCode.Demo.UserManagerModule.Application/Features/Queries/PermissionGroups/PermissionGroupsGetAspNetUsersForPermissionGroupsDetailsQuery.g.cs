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
    public class PermissionGroupsGetAspNetUsersForPermissionGroupsDetailsQuery : IRequest<Response<PermissionGroupsGetAspNetUsersForPermissionGroupsResponseDto>>
    {
        public string PermissionGroupsName { get; set; }
        public string AspNetUsersId { get; set; }

        public PermissionGroupsGetAspNetUsersForPermissionGroupsDetailsQuery(string permissionGroupsName, string aspNetUsersId)
        {
            this.PermissionGroupsName = permissionGroupsName;
            this.AspNetUsersId = aspNetUsersId;
        }

        public class PermissionGroupsGetAspNetUsersForPermissionGroupsDetailsHandler : IRequestHandler<PermissionGroupsGetAspNetUsersForPermissionGroupsDetailsQuery, Response<PermissionGroupsGetAspNetUsersForPermissionGroupsResponseDto>>
        {
            private readonly ILogger<PermissionGroupsGetAspNetUsersForPermissionGroupsDetailsHandler> _logger;
            private readonly IPermissionGroupsRepository _repository;
            public PermissionGroupsGetAspNetUsersForPermissionGroupsDetailsHandler(ILogger<PermissionGroupsGetAspNetUsersForPermissionGroupsDetailsHandler> logger, IPermissionGroupsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PermissionGroupsGetAspNetUsersForPermissionGroupsResponseDto>> Handle(PermissionGroupsGetAspNetUsersForPermissionGroupsDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PermissionGroupsGetAspNetUsersForPermissionGroupsDetailsAsync(request.PermissionGroupsName, request.AspNetUsersId);
                return returnValue.ToResponse();
            }
        }
    }
}