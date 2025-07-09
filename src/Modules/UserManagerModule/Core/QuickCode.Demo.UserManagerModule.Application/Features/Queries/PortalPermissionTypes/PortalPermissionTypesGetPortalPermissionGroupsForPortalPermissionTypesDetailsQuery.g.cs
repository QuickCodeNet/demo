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
    public class PortalPermissionTypesGetPortalPermissionGroupsForPortalPermissionTypesDetailsQuery : IRequest<Response<PortalPermissionTypesGetPortalPermissionGroupsForPortalPermissionTypesResponseDto>>
    {
        public int PortalPermissionTypesId { get; set; }
        public int PortalPermissionGroupsId { get; set; }

        public PortalPermissionTypesGetPortalPermissionGroupsForPortalPermissionTypesDetailsQuery(int portalPermissionTypesId, int portalPermissionGroupsId)
        {
            this.PortalPermissionTypesId = portalPermissionTypesId;
            this.PortalPermissionGroupsId = portalPermissionGroupsId;
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
                var returnValue = await _repository.PortalPermissionTypesGetPortalPermissionGroupsForPortalPermissionTypesDetailsAsync(request.PortalPermissionTypesId, request.PortalPermissionGroupsId);
                return returnValue.ToResponse();
            }
        }
    }
}