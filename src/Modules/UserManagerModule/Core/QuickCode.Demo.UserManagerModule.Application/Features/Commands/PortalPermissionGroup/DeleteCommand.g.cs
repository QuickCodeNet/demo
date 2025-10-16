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
    public class DeletePortalPermissionGroupCommand : IRequest<Response<bool>>
    {
        public PortalPermissionGroupDto request { get; set; }

        public DeletePortalPermissionGroupCommand(PortalPermissionGroupDto request)
        {
            this.request = request;
        }

        public class DeletePortalPermissionGroupHandler : IRequestHandler<DeletePortalPermissionGroupCommand, Response<bool>>
        {
            private readonly ILogger<DeletePortalPermissionGroupHandler> _logger;
            private readonly IPortalPermissionGroupRepository _repository;
            public DeletePortalPermissionGroupHandler(ILogger<DeletePortalPermissionGroupHandler> logger, IPortalPermissionGroupRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeletePortalPermissionGroupCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}