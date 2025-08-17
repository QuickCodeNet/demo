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
    public class PortalPermissionGroupsGetPortalPermissionGroupsQuery : IRequest<Response<List<PortalPermissionGroupsGetPortalPermissionGroupsResponseDto>>>
    {
        public string PortalPermissionGroupsPermissionGroupName { get; set; }

        public PortalPermissionGroupsGetPortalPermissionGroupsQuery(string portalPermissionGroupsPermissionGroupName)
        {
            this.PortalPermissionGroupsPermissionGroupName = portalPermissionGroupsPermissionGroupName;
        }

        public class PortalPermissionGroupsGetPortalPermissionGroupsHandler : IRequestHandler<PortalPermissionGroupsGetPortalPermissionGroupsQuery, Response<List<PortalPermissionGroupsGetPortalPermissionGroupsResponseDto>>>
        {
            private readonly ILogger<PortalPermissionGroupsGetPortalPermissionGroupsHandler> _logger;
            private readonly IPortalPermissionGroupsRepository _repository;
            public PortalPermissionGroupsGetPortalPermissionGroupsHandler(ILogger<PortalPermissionGroupsGetPortalPermissionGroupsHandler> logger, IPortalPermissionGroupsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<PortalPermissionGroupsGetPortalPermissionGroupsResponseDto>>> Handle(PortalPermissionGroupsGetPortalPermissionGroupsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PortalPermissionGroupsGetPortalPermissionGroupsAsync(request.PortalPermissionGroupsPermissionGroupName);
                return returnValue.ToResponse();
            }
        }
    }
}