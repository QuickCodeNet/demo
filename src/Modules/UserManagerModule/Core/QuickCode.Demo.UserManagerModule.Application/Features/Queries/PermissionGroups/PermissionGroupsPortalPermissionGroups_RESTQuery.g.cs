using System;
using System.Linq;
using MediatR;
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
    public class PermissionGroupsPermissionGroupsPortalPermissionGroups_RESTQuery : IRequest<Response<List<PermissionGroupsPortalPermissionGroups_RESTResponseDto>>>
    {
        public int PermissionGroupsId { get; set; }

        public PermissionGroupsPermissionGroupsPortalPermissionGroups_RESTQuery(int permissionGroupsId)
        {
            this.PermissionGroupsId = permissionGroupsId;
        }

        public class PermissionGroupsPermissionGroupsPortalPermissionGroups_RESTHandler : IRequestHandler<PermissionGroupsPermissionGroupsPortalPermissionGroups_RESTQuery, Response<List<PermissionGroupsPortalPermissionGroups_RESTResponseDto>>>
        {
            private readonly ILogger<PermissionGroupsPermissionGroupsPortalPermissionGroups_RESTHandler> _logger;
            private readonly IPermissionGroupsRepository _repository;
            public PermissionGroupsPermissionGroupsPortalPermissionGroups_RESTHandler(ILogger<PermissionGroupsPermissionGroupsPortalPermissionGroups_RESTHandler> logger, IPermissionGroupsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<PermissionGroupsPortalPermissionGroups_RESTResponseDto>>> Handle(PermissionGroupsPermissionGroupsPortalPermissionGroups_RESTQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PermissionGroupsPortalPermissionGroups_RESTAsync(request.PermissionGroupsId);
                return returnValue.ToResponse();
            }
        }
    }
}