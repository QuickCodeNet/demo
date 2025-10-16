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
    public class UpdatePortalPermissionGroupCommand : IRequest<Response<bool>>
    {
        public string PortalPermissionName { get; set; }
        public string PermissionGroupName { get; set; }
        public int PortalPermissionTypeId { get; set; }
        public PortalPermissionGroupDto request { get; set; }

        public UpdatePortalPermissionGroupCommand(string portalPermissionName, string permissionGroupName, int portalPermissionTypeId, PortalPermissionGroupDto request)
        {
            this.request = request;
            this.PortalPermissionName = portalPermissionName;
            this.PermissionGroupName = permissionGroupName;
            this.PortalPermissionTypeId = portalPermissionTypeId;
        }

        public class UpdatePortalPermissionGroupHandler : IRequestHandler<UpdatePortalPermissionGroupCommand, Response<bool>>
        {
            private readonly ILogger<UpdatePortalPermissionGroupHandler> _logger;
            private readonly IPortalPermissionGroupRepository _repository;
            public UpdatePortalPermissionGroupHandler(ILogger<UpdatePortalPermissionGroupHandler> logger, IPortalPermissionGroupRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(UpdatePortalPermissionGroupCommand request, CancellationToken cancellationToken)
            {
                var updateItem = await _repository.GetByPkAsync(request.PortalPermissionName, request.PermissionGroupName, request.PortalPermissionTypeId);
                if (updateItem.Code == 404)
                    return Response<bool>.NotFound();
                var model = request.request;
                var returnValue = await _repository.UpdateAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}