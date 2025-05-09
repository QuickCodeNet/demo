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
    public class PortalPermissionGroupsPortalPermissionGroupsGetPortalPermissionGroupsQuery : IRequest<Response<List<PortalPermissionGroupsGetPortalPermissionGroupsResponseDto>>>
    {
        public int PortalPermissionGroupsPermissionGroupId { get; set; }

        public PortalPermissionGroupsPortalPermissionGroupsGetPortalPermissionGroupsQuery(int portalPermissionGroupsPermissionGroupId)
        {
            this.PortalPermissionGroupsPermissionGroupId = portalPermissionGroupsPermissionGroupId;
        }

        public class PortalPermissionGroupsPortalPermissionGroupsGetPortalPermissionGroupsHandler : IRequestHandler<PortalPermissionGroupsPortalPermissionGroupsGetPortalPermissionGroupsQuery, Response<List<PortalPermissionGroupsGetPortalPermissionGroupsResponseDto>>>
        {
            private readonly ILogger<PortalPermissionGroupsPortalPermissionGroupsGetPortalPermissionGroupsHandler> _logger;
            private readonly IPortalPermissionGroupsRepository _repository;
            public PortalPermissionGroupsPortalPermissionGroupsGetPortalPermissionGroupsHandler(ILogger<PortalPermissionGroupsPortalPermissionGroupsGetPortalPermissionGroupsHandler> logger, IPortalPermissionGroupsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<PortalPermissionGroupsGetPortalPermissionGroupsResponseDto>>> Handle(PortalPermissionGroupsPortalPermissionGroupsGetPortalPermissionGroupsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PortalPermissionGroupsGetPortalPermissionGroupsAsync(request.PortalPermissionGroupsPermissionGroupId);
                return returnValue.ToResponse();
            }
        }
    }
}