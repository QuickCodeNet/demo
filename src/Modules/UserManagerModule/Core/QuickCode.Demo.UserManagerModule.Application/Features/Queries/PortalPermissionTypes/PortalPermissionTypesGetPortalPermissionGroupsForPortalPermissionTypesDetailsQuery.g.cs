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
    public class PortalPermissionTypesGetPortalPermissionGroupsForPortalPermissionTypesDetailsQuery : IRequest<Response<PortalPermissionTypesGetPortalPermissionGroupsForPortalPermissionTypesResponseDto>>
    {
        public int PortalPermissionTypesId { get; set; }
        public string PortalPermissionGroupsPortalPermissionName { get; set; }

        public PortalPermissionTypesGetPortalPermissionGroupsForPortalPermissionTypesDetailsQuery(int portalPermissionTypesId, string portalPermissionGroupsPortalPermissionName)
        {
            this.PortalPermissionTypesId = portalPermissionTypesId;
            this.PortalPermissionGroupsPortalPermissionName = portalPermissionGroupsPortalPermissionName;
        }

        public class PortalPermissionTypesGetPortalPermissionGroupsForPortalPermissionTypesDetailsHandler : IRequestHandler<PortalPermissionTypesGetPortalPermissionGroupsForPortalPermissionTypesDetailsQuery, Response<PortalPermissionTypesGetPortalPermissionGroupsForPortalPermissionTypesResponseDto>>
        {
            private readonly ILogger<PortalPermissionTypesGetPortalPermissionGroupsForPortalPermissionTypesDetailsHandler> _logger;
            private readonly IPortalPermissionTypesRepository _repository;
            public PortalPermissionTypesGetPortalPermissionGroupsForPortalPermissionTypesDetailsHandler(ILogger<PortalPermissionTypesGetPortalPermissionGroupsForPortalPermissionTypesDetailsHandler> logger, IPortalPermissionTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PortalPermissionTypesGetPortalPermissionGroupsForPortalPermissionTypesResponseDto>> Handle(PortalPermissionTypesGetPortalPermissionGroupsForPortalPermissionTypesDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PortalPermissionTypesGetPortalPermissionGroupsForPortalPermissionTypesDetailsAsync(request.PortalPermissionTypesId, request.PortalPermissionGroupsPortalPermissionName);
                return returnValue.ToResponse();
            }
        }
    }
}