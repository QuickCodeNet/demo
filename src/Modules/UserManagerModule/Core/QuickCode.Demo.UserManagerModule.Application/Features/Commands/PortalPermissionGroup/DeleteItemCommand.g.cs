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
    public class DeleteItemPortalPermissionGroupCommand : IRequest<Response<bool>>
    {
        public string PortalPermissionName { get; set; }
        public string PermissionGroupName { get; set; }
        public int PortalPermissionTypeId { get; set; }

        public DeleteItemPortalPermissionGroupCommand(string portalPermissionName, string permissionGroupName, int portalPermissionTypeId)
        {
            this.PortalPermissionName = portalPermissionName;
            this.PermissionGroupName = permissionGroupName;
            this.PortalPermissionTypeId = portalPermissionTypeId;
        }

        public class DeleteItemPortalPermissionGroupHandler : IRequestHandler<DeleteItemPortalPermissionGroupCommand, Response<bool>>
        {
            private readonly ILogger<DeleteItemPortalPermissionGroupHandler> _logger;
            private readonly IPortalPermissionGroupRepository _repository;
            public DeleteItemPortalPermissionGroupHandler(ILogger<DeleteItemPortalPermissionGroupHandler> logger, IPortalPermissionGroupRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeleteItemPortalPermissionGroupCommand request, CancellationToken cancellationToken)
            {
                var deleteItem = await _repository.GetByPkAsync(request.PortalPermissionName, request.PermissionGroupName, request.PortalPermissionTypeId);
                if (deleteItem.Code == 404)
                    return Response<bool>.NotFound();
                var returnValue = await _repository.DeleteAsync(deleteItem.Value);
                return returnValue.ToResponse();
            }
        }
    }
}