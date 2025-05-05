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
    public class PortalPermissionTypesPortalPermissionTypesPortalPermissionGroups_RESTQuery : IRequest<Response<List<PortalPermissionTypesPortalPermissionGroups_RESTResponseDto>>>
    {
        public int PortalPermissionTypesId { get; set; }

        public PortalPermissionTypesPortalPermissionTypesPortalPermissionGroups_RESTQuery(int portalPermissionTypesId)
        {
            this.PortalPermissionTypesId = portalPermissionTypesId;
        }

        public class PortalPermissionTypesPortalPermissionTypesPortalPermissionGroups_RESTHandler : IRequestHandler<PortalPermissionTypesPortalPermissionTypesPortalPermissionGroups_RESTQuery, Response<List<PortalPermissionTypesPortalPermissionGroups_RESTResponseDto>>>
        {
            private readonly ILogger<PortalPermissionTypesPortalPermissionTypesPortalPermissionGroups_RESTHandler> _logger;
            private readonly IPortalPermissionTypesRepository _repository;
            public PortalPermissionTypesPortalPermissionTypesPortalPermissionGroups_RESTHandler(ILogger<PortalPermissionTypesPortalPermissionTypesPortalPermissionGroups_RESTHandler> logger, IPortalPermissionTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<PortalPermissionTypesPortalPermissionGroups_RESTResponseDto>>> Handle(PortalPermissionTypesPortalPermissionTypesPortalPermissionGroups_RESTQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.PortalPermissionTypesPortalPermissionGroups_RESTAsync(request.PortalPermissionTypesId);
                return returnValue.ToResponse();
            }
        }
    }
}