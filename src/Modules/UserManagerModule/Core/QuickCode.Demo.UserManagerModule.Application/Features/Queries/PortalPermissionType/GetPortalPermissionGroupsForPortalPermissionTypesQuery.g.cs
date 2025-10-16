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
    public class GetPortalPermissionGroupsForPortalPermissionTypesQuery : IRequest<Response<List<GetPortalPermissionGroupsForPortalPermissionTypesResponseDto>>>
    {
        public int PortalPermissionTypesId { get; set; }

        public GetPortalPermissionGroupsForPortalPermissionTypesQuery(int portalPermissionTypesId)
        {
            this.PortalPermissionTypesId = portalPermissionTypesId;
        }

        public class GetPortalPermissionGroupsForPortalPermissionTypesHandler : IRequestHandler<GetPortalPermissionGroupsForPortalPermissionTypesQuery, Response<List<GetPortalPermissionGroupsForPortalPermissionTypesResponseDto>>>
        {
            private readonly ILogger<GetPortalPermissionGroupsForPortalPermissionTypesHandler> _logger;
            private readonly IPortalPermissionTypeRepository _repository;
            public GetPortalPermissionGroupsForPortalPermissionTypesHandler(ILogger<GetPortalPermissionGroupsForPortalPermissionTypesHandler> logger, IPortalPermissionTypeRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<GetPortalPermissionGroupsForPortalPermissionTypesResponseDto>>> Handle(GetPortalPermissionGroupsForPortalPermissionTypesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetPortalPermissionGroupsForPortalPermissionTypesAsync(request.PortalPermissionTypesId);
                return returnValue.ToResponse();
            }
        }
    }
}