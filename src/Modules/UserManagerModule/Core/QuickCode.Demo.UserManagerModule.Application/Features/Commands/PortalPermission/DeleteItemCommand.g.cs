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
    public class DeleteItemPortalPermissionCommand : IRequest<Response<bool>>
    {
        public string Name { get; set; }

        public DeleteItemPortalPermissionCommand(string name)
        {
            this.Name = name;
        }

        public class DeleteItemPortalPermissionHandler : IRequestHandler<DeleteItemPortalPermissionCommand, Response<bool>>
        {
            private readonly ILogger<DeleteItemPortalPermissionHandler> _logger;
            private readonly IPortalPermissionRepository _repository;
            public DeleteItemPortalPermissionHandler(ILogger<DeleteItemPortalPermissionHandler> logger, IPortalPermissionRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeleteItemPortalPermissionCommand request, CancellationToken cancellationToken)
            {
                var deleteItem = await _repository.GetByPkAsync(request.Name);
                if (deleteItem.Code == 404)
                    return Response<bool>.NotFound();
                var returnValue = await _repository.DeleteAsync(deleteItem.Value);
                return returnValue.ToResponse();
            }
        }
    }
}