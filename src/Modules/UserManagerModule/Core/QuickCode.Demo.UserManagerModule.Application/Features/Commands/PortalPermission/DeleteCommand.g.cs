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
using QuickCode.Demo.UserManagerModule.Application.Dtos.PortalPermission;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.PortalPermission
{
    public class DeletePortalPermissionCommand : IRequest<Response<bool>>
    {
        public PortalPermissionDto request { get; set; }

        public DeletePortalPermissionCommand(PortalPermissionDto request)
        {
            this.request = request;
        }

        public class DeletePortalPermissionHandler : IRequestHandler<DeletePortalPermissionCommand, Response<bool>>
        {
            private readonly ILogger<DeletePortalPermissionHandler> _logger;
            private readonly IPortalPermissionRepository _repository;
            public DeletePortalPermissionHandler(ILogger<DeletePortalPermissionHandler> logger, IPortalPermissionRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeletePortalPermissionCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}