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
    public class DeletePortalPermissionTypeCommand : IRequest<Response<bool>>
    {
        public PortalPermissionTypeDto request { get; set; }

        public DeletePortalPermissionTypeCommand(PortalPermissionTypeDto request)
        {
            this.request = request;
        }

        public class DeletePortalPermissionTypeHandler : IRequestHandler<DeletePortalPermissionTypeCommand, Response<bool>>
        {
            private readonly ILogger<DeletePortalPermissionTypeHandler> _logger;
            private readonly IPortalPermissionTypeRepository _repository;
            public DeletePortalPermissionTypeHandler(ILogger<DeletePortalPermissionTypeHandler> logger, IPortalPermissionTypeRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeletePortalPermissionTypeCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}