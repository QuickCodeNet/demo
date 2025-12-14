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
using QuickCode.Demo.UserManagerModule.Application.Dtos.PortalPermissionGroup;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.PortalPermissionGroup
{
    public class GetPortalPermissionGroupsQuery : IRequest<Response<List<GetPortalPermissionGroupsResponseDto>>>
    {
        public string PortalPermissionGroupsPermissionGroupName { get; set; }

        public GetPortalPermissionGroupsQuery(string portalPermissionGroupsPermissionGroupName)
        {
            this.PortalPermissionGroupsPermissionGroupName = portalPermissionGroupsPermissionGroupName;
        }

        public class GetPortalPermissionGroupsHandler : IRequestHandler<GetPortalPermissionGroupsQuery, Response<List<GetPortalPermissionGroupsResponseDto>>>
        {
            private readonly ILogger<GetPortalPermissionGroupsHandler> _logger;
            private readonly IPortalPermissionGroupRepository _repository;
            public GetPortalPermissionGroupsHandler(ILogger<GetPortalPermissionGroupsHandler> logger, IPortalPermissionGroupRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetPortalPermissionGroupsResponseDto>>> Handle(GetPortalPermissionGroupsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetPortalPermissionGroupsAsync(request.PortalPermissionGroupsPermissionGroupName);
                return returnValue.ToResponse();
            }
        }
    }
}