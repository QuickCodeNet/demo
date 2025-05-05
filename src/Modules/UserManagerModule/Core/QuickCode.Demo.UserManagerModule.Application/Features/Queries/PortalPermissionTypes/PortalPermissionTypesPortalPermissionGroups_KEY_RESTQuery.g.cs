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
    public class PortalPermissionTypesPortalPermissionTypesPortalPermissionGroups_KEY_RESTQuery : IRequest<Response<PortalPermissionTypesPortalPermissionGroups_KEY_RESTResponseDto>>
    {
        public int PortalPermissionTypesId { get; set; }
        public int PortalPermissionGroupsId { get; set; }

        public PortalPermissionTypesPortalPermissionTypesPortalPermissionGroups_KEY_RESTQuery(int portalPermissionTypesId, int portalPermissionGroupsId)
        {
            this.PortalPermissionTypesId = portalPermissionTypesId;
            this.PortalPermissionGroupsId = portalPermissionGroupsId;
        }

        public class PortalPermissionTypesPortalPermissionTypesPortalPermissionGroups_KEY_RESTHandler : IRequestHandler<PortalPermissionTypesPortalPermissionTypesPortalPermissionGroups_KEY_RESTQuery, Response<PortalPermissionTypesPortalPermissionGroups_KEY_RESTResponseDto>>
        {
            private readonly ILogger<PortalPermissionTypesPortalPermissionTypesPortalPermissionGroups_KEY_RESTHandler> _logger;
            private readonly IPortalPermissionTypesRepository _repository;
            public PortalPermissionTypesPortalPermissionTypesPortalPermissionGroups_KEY_RESTHandler(ILogger<PortalPermissionTypesPortalPermissionTypesPortalPermissionGroups_KEY_RESTHandler> logger, IPortalPermissionTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<PortalPermissionTypesPortalPermissionGroups_KEY_RESTResponseDto>> Handle(PortalPermissionTypesPortalPermissionTypesPortalPermissionGroups_KEY_RESTQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PortalPermissionTypesPortalPermissionGroups_KEY_RESTAsync(request.PortalPermissionTypesId, request.PortalPermissionGroupsId);
                return returnValue.ToResponse();
            }
        }
    }
}