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
    public class GetAspNetUsersForPermissionGroupsDetailsQuery : IRequest<Response<GetAspNetUsersForPermissionGroupsResponseDto>>
    {
        public string PermissionGroupsName { get; set; }
        public string AspNetUsersId { get; set; }

        public GetAspNetUsersForPermissionGroupsDetailsQuery(string permissionGroupsName, string aspNetUsersId)
        {
            this.PermissionGroupsName = permissionGroupsName;
            this.AspNetUsersId = aspNetUsersId;
        }

        public class GetAspNetUsersForPermissionGroupsDetailsHandler : IRequestHandler<GetAspNetUsersForPermissionGroupsDetailsQuery, Response<GetAspNetUsersForPermissionGroupsResponseDto>>
        {
            private readonly ILogger<GetAspNetUsersForPermissionGroupsDetailsHandler> _logger;
            private readonly IPermissionGroupRepository _repository;
            public GetAspNetUsersForPermissionGroupsDetailsHandler(ILogger<GetAspNetUsersForPermissionGroupsDetailsHandler> logger, IPermissionGroupRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<GetAspNetUsersForPermissionGroupsResponseDto>> Handle(GetAspNetUsersForPermissionGroupsDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetAspNetUsersForPermissionGroupsDetailsAsync(request.PermissionGroupsName, request.AspNetUsersId);
                return returnValue.ToResponse();
            }
        }
    }
}