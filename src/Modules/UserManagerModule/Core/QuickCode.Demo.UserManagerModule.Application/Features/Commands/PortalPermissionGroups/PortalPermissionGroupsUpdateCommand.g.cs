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
    public class PortalPermissionGroupsUpdateCommand : IRequest<Response<bool>>
    {
        public string PortalPermissionName { get; set; }
        public string PermissionGroupName { get; set; }
        public int PortalPermissionTypeId { get; set; }
        public PortalPermissionGroupsDto request { get; set; }

        public PortalPermissionGroupsUpdateCommand(string portalPermissionName, string permissionGroupName, int portalPermissionTypeId, PortalPermissionGroupsDto request)
        {
            this.request = request;
            this.PortalPermissionName = portalPermissionName;
            this.PermissionGroupName = permissionGroupName;
            this.PortalPermissionTypeId = portalPermissionTypeId;
        }

        public class PortalPermissionGroupsUpdateHandler : IRequestHandler<PortalPermissionGroupsUpdateCommand, Response<bool>>
        {
            private readonly ILogger<PortalPermissionGroupsUpdateHandler> _logger;
            private readonly IPortalPermissionGroupsRepository _repository;
            public PortalPermissionGroupsUpdateHandler(ILogger<PortalPermissionGroupsUpdateHandler> logger, IPortalPermissionGroupsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(PortalPermissionGroupsUpdateCommand request, CancellationToken cancellationToken)
            {
                var updateItem = await _repository.GetByPkAsync(request.PortalPermissionName, request.PermissionGroupName, request.PortalPermissionTypeId);
                if (updateItem.Code == 404)
                {
                    return new Response<bool>()
                    {
                        Code = 404,
                        Value = false
                    };
                }

                var model = request.request;
                var returnValue = await _repository.UpdateAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}