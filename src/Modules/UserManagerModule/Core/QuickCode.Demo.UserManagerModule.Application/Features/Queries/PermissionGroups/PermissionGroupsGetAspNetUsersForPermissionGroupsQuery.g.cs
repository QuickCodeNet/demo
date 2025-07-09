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

namespace QuickCode.Demo.UserManagerModule.Application.Features
{
    public class PermissionGroupsGetAspNetUsersForPermissionGroupsQuery : IRequest<Response<List<PermissionGroupsGetAspNetUsersForPermissionGroupsResponseDto>>>
    {
        public int PermissionGroupsId { get; set; }

        public PermissionGroupsGetAspNetUsersForPermissionGroupsQuery(int permissionGroupsId)
        {
            this.PermissionGroupsId = permissionGroupsId;
        }

        public class PermissionGroupsGetAspNetUsersForPermissionGroupsHandler : IRequestHandler<PermissionGroupsGetAspNetUsersForPermissionGroupsQuery, Response<List<PermissionGroupsGetAspNetUsersForPermissionGroupsResponseDto>>>
        {
            private readonly ILogger<PermissionGroupsGetAspNetUsersForPermissionGroupsHandler> _logger;
            private readonly IPermissionGroupsRepository _repository;
            public PermissionGroupsGetAspNetUsersForPermissionGroupsHandler(ILogger<PermissionGroupsGetAspNetUsersForPermissionGroupsHandler> logger, IPermissionGroupsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<PermissionGroupsGetAspNetUsersForPermissionGroupsResponseDto>>> Handle(PermissionGroupsGetAspNetUsersForPermissionGroupsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PermissionGroupsGetAspNetUsersForPermissionGroupsAsync(request.PermissionGroupsId);
                return returnValue.ToResponse();
            }
        }
    }
}