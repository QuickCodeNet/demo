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
    public class PortalPermissionTypesGetPortalPermissionGroupsForPortalPermissionTypesQuery : IRequest<Response<List<PortalPermissionTypesGetPortalPermissionGroupsForPortalPermissionTypesResponseDto>>>
    {
        public int PortalPermissionTypesId { get; set; }

        public PortalPermissionTypesGetPortalPermissionGroupsForPortalPermissionTypesQuery(int portalPermissionTypesId)
        {
            this.PortalPermissionTypesId = portalPermissionTypesId;
        }

        public class PortalPermissionTypesGetPortalPermissionGroupsForPortalPermissionTypesHandler : IRequestHandler<PortalPermissionTypesGetPortalPermissionGroupsForPortalPermissionTypesQuery, Response<List<PortalPermissionTypesGetPortalPermissionGroupsForPortalPermissionTypesResponseDto>>>
        {
            private readonly ILogger<PortalPermissionTypesGetPortalPermissionGroupsForPortalPermissionTypesHandler> _logger;
            private readonly IPortalPermissionTypesRepository _repository;
            public PortalPermissionTypesGetPortalPermissionGroupsForPortalPermissionTypesHandler(ILogger<PortalPermissionTypesGetPortalPermissionGroupsForPortalPermissionTypesHandler> logger, IPortalPermissionTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<PortalPermissionTypesGetPortalPermissionGroupsForPortalPermissionTypesResponseDto>>> Handle(PortalPermissionTypesGetPortalPermissionGroupsForPortalPermissionTypesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PortalPermissionTypesGetPortalPermissionGroupsForPortalPermissionTypesAsync(request.PortalPermissionTypesId);
                return returnValue.ToResponse();
            }
        }
    }
}