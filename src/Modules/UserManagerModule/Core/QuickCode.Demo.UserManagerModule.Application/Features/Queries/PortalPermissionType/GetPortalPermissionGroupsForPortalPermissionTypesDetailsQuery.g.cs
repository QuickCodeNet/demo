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
using QuickCode.Demo.UserManagerModule.Application.Dtos.PortalPermissionType;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.PortalPermissionType
{
    public class GetPortalPermissionGroupsForPortalPermissionTypesDetailsQuery : IRequest<Response<GetPortalPermissionGroupsForPortalPermissionTypesResponseDto>>
    {
        public int PortalPermissionTypesId { get; set; }
        public string PortalPermissionGroupsPortalPermissionName { get; set; }

        public GetPortalPermissionGroupsForPortalPermissionTypesDetailsQuery(int portalPermissionTypesId, string portalPermissionGroupsPortalPermissionName)
        {
            this.PortalPermissionTypesId = portalPermissionTypesId;
            this.PortalPermissionGroupsPortalPermissionName = portalPermissionGroupsPortalPermissionName;
        }

        public class GetPortalPermissionGroupsForPortalPermissionTypesDetailsHandler : IRequestHandler<GetPortalPermissionGroupsForPortalPermissionTypesDetailsQuery, Response<GetPortalPermissionGroupsForPortalPermissionTypesResponseDto>>
        {
            private readonly ILogger<GetPortalPermissionGroupsForPortalPermissionTypesDetailsHandler> _logger;
            private readonly IPortalPermissionTypeRepository _repository;
            public GetPortalPermissionGroupsForPortalPermissionTypesDetailsHandler(ILogger<GetPortalPermissionGroupsForPortalPermissionTypesDetailsHandler> logger, IPortalPermissionTypeRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<GetPortalPermissionGroupsForPortalPermissionTypesResponseDto>> Handle(GetPortalPermissionGroupsForPortalPermissionTypesDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetPortalPermissionGroupsForPortalPermissionTypesDetailsAsync(request.PortalPermissionTypesId, request.PortalPermissionGroupsPortalPermissionName);
                return returnValue.ToResponse();
            }
        }
    }
}