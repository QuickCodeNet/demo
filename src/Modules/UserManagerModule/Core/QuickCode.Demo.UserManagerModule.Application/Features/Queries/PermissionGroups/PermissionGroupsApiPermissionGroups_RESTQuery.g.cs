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
    public class PermissionGroupsPermissionGroupsApiPermissionGroups_RESTQuery : IRequest<Response<List<PermissionGroupsApiPermissionGroups_RESTResponseDto>>>
    {
        public int PermissionGroupsId { get; set; }

        public PermissionGroupsPermissionGroupsApiPermissionGroups_RESTQuery(int permissionGroupsId)
        {
            this.PermissionGroupsId = permissionGroupsId;
        }

        public class PermissionGroupsPermissionGroupsApiPermissionGroups_RESTHandler : IRequestHandler<PermissionGroupsPermissionGroupsApiPermissionGroups_RESTQuery, Response<List<PermissionGroupsApiPermissionGroups_RESTResponseDto>>>
        {
            private readonly ILogger<PermissionGroupsPermissionGroupsApiPermissionGroups_RESTHandler> _logger;
            private readonly IPermissionGroupsRepository _repository;
            public PermissionGroupsPermissionGroupsApiPermissionGroups_RESTHandler(ILogger<PermissionGroupsPermissionGroupsApiPermissionGroups_RESTHandler> logger, IPermissionGroupsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<PermissionGroupsApiPermissionGroups_RESTResponseDto>>> Handle(PermissionGroupsPermissionGroupsApiPermissionGroups_RESTQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PermissionGroupsApiPermissionGroups_RESTAsync(request.PermissionGroupsId);
                return returnValue.ToResponse();
            }
        }
    }
}